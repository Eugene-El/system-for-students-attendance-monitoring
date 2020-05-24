using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Entities;
using SAMS.Database.EF;
using SAMS.Database.EF.EntityFramework;
using SAMS.Notification.Service.Configs;
using SAMS.Notification.Service.Models;
using SAMS.Notification.Service.Notificators;

namespace SAMS.Notification.Service
{
    public class Worker : BackgroundService
    {
        private const int serviceInnerSleepTime = 10000;

        private ILogger<Worker> logger;
        private IServiceScopeFactory serviceScopeFactory;
        private EmailNotificationConfig emailNotificationConfig;
        private SmsNotificationConfig smsNotificationConfig;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory, EmailNotificationConfig emailNotificationConfig, SmsNotificationConfig smsNotificationConfig)
        {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
            this.emailNotificationConfig = emailNotificationConfig;
            this.smsNotificationConfig = smsNotificationConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                    await Work(new DatabaseEF(dataContext));
                }

                await Task.Delay(serviceInnerSleepTime, stoppingToken);
            }
        }

        private async Task Work(IDatabase database)
        {
            var configuration = database.ConfigurationService.GetNotificationAndSyncConfiguration();
            var lastNotificationTime = database.ConfigurationService.GetLastNotificationTime();

            logger.LogInformation("Last notification time: {0}", lastNotificationTime);
            var now = DateTime.UtcNow;
            if (!lastNotificationTime.HasValue || lastNotificationTime.Value.AddHours(configuration.NotificationsPeriodHours) <= now)
            {
                lastNotificationTime = now;

                await NotifyStudents(database);

                database.ConfigurationService.SetLastNotificationTime(now);
            }
            logger.LogInformation("Next notification time: {0}", lastNotificationTime.Value.AddHours(configuration.NotificationsPeriodHours));
        }

        private async Task NotifyStudents(IDatabase database)
        {
            try
            {
                logger.LogInformation("Notification started.");



                database.NotificationRuleService.GetAll().ToList().ForEach(rule => {

                    var to = DateTime.Today;
                    var from = to.AddDays(rule.AttendancePeriod * -1);

                    var studentIds = database.StudentService.GetAll()
                        .Where(st => st.StudyProgrammeId == rule.StudyProgrammeId &&
                            st.Language == rule.Language && st.LearningForm == rule.LearningForm)
                        .Select(s => s.Id).ToList();

                    studentIds.ForEach(studentId =>
                    {
                        var attendances = database.StudentAttendanceService.GetAllByStudentId(studentId)
                            .Where(a => a.Date >= from && a.Date <= to);

                        if (attendances.Any())
                        {
                            var procent = 100d * attendances.Sum(a => a.RealAttendance) / attendances.Sum(a => a.NecessaryAttendance);
                            if (procent <= rule.AttendanceProcent)
                            {
                                var student = database.StudentService.Get(studentId);
                                // Notify
                                var message = InsertDataIntoMessage(rule.Message, student, procent);

                                NotificationResult result = new NotificationResult();
                                switch (rule.NotificationMethod)
                                {
                                    case BusinessLogic.Entities.Enumerations.NotificationMethod.Email:
                                    {
                                        var emails = new List<string>();
                                        if (!string.IsNullOrEmpty(student.Email1))
                                            emails.Add(student.Email1);
                                        if (!string.IsNullOrEmpty(student.Email2))
                                            emails.Add(student.Email2);
                                        if (emails.Any())
                                            result = EmailNotificator.Notify(emails.ToArray(), message);
                                        break;
                                    }
                                    case BusinessLogic.Entities.Enumerations.NotificationMethod.SMS:
                                    {
                                        var emails = new List<string>();
                                        if (!string.IsNullOrEmpty(student.Phone1))
                                            emails.Add(student.Phone1);
                                        if (!string.IsNullOrEmpty(student.Phone2))
                                            emails.Add(student.Phone2);
                                        if (emails.Any())
                                            result = SmsNotificator.Notify(emails.ToArray(), message);
                                        break;
                                    }
                                }

                                database.NotificationHistoryService.Add(new NotificationHistory
                                {
                                    Id = 0,
                                    StudentId = studentId,
                                    Message = message,
                                    SendingTime = DateTime.UtcNow,
                                    Status = result.NotificationStatus,
                                    ErrorMessage = result.ErrorMessage
                                });
                            }
                        }
                    });


                });

                logger.LogInformation("Notification ended.");
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }

        protected EmailNotificator emailNotificator;
        protected EmailNotificator EmailNotificator {
            get {
                if (emailNotificator == null)
                    emailNotificator = new EmailNotificator(emailNotificationConfig);
                return emailNotificator;
            }
        }
        protected SmsNotificator smsNotificator;
        protected SmsNotificator SmsNotificator
        {
            get
            {
                if (smsNotificator == null)
                    smsNotificator = new SmsNotificator(smsNotificationConfig);
                return smsNotificator;
            }
        }

        private string InsertDataIntoMessage(string message, Student student, double procent)
        {
            return message
                .Replace("{{Name}}", student.Name)
                .Replace("{{Surname}}", student.Surname)
                .Replace("{{StudentCode}}", student.Code)
                .Replace("{{AttendancePercent}}", procent.ToString("0.##"));
        }
    }
}
