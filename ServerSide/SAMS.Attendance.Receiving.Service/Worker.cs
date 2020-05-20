using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using SAMS.Attendance.Receiving.Service.ExternalModels;
using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Entities;
using SAMS.BusinessLogic.Entities.Enumerations;
using SAMS.Database.EF;
using SAMS.Database.EF.EntityFramework;

namespace SAMS.Attendance.Receiving.Service
{
    public class Worker : BackgroundService
    {
        private const int serviceInnerSleepTime = 10000;

        private ILogger<Worker> logger;
        private IServiceScopeFactory serviceScopeFactory;
        private IOptions<ApiPaths> config;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory, IOptions<ApiPaths> config)
        {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
            this.config = config;
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
            var lastSyncTime = database.ConfigurationService.GetLastDataSyncTime();

            logger.LogInformation("Last data-sync time: {0}", lastSyncTime);
            var now = DateTime.UtcNow;
            if (!lastSyncTime.HasValue || lastSyncTime.Value.AddHours(configuration.DataSyncPeriodHours) <= now)
            {
                lastSyncTime = now;
                await RequestExternalApi(database);
                database.ConfigurationService.SetLastDataSyncTime(now);
            }
            logger.LogInformation("Next data-sync time: {0}", lastSyncTime.Value.AddHours(configuration.DataSyncPeriodHours));
        }

        private async Task RequestExternalApi(IDatabase database)
        {
            try
            {
                logger.LogInformation("Data-sync started.");

                await RequestSubjects(database);
                await RequestInstituteStucture(database);
                await RequestStudentAtteandance(database);

                logger.LogInformation("Data-sync ended.");
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }


        private async Task RequestSubjects(IDatabase database)
        {
            try
            {
                var client = new RestClient(config.Value.BaseApiPath);
                var request = new RestRequest(config.Value.GetSubjectsRequest, Method.GET);
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var content = JsonConvert.DeserializeObject<List<SubjectExternalModel>>(response.Content);

                    if (content == null)
                        return;

                    var mappedData = content.Select(s => new Subject
                    {
                        Code = s.SubjectID.ToString(),
                        TitleEn = s.Title_EN,
                        TitleLv = s.Title_LV,
                        TitleRu = s.Title_RU,
                        ShortTitleEn = s.Short_title_EN,
                        ShortTitleLv = s.Short_title_LV,
                        ShortTitleRu = s.Short_title_RU
                    });

                    var dataFromDb = database.SubjectService.GetAll();

                    var dataToImport = mappedData.Where(m => !dataFromDb.Any(d => d.Code == m.Code));
                    if (dataToImport.Any())
                        foreach (var subject in dataToImport)
                            database.SubjectService.Add(subject);

                    logger.LogInformation("Subjects imported");
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }
        private async Task RequestInstituteStucture(IDatabase database)
        {
            try
            {
                var client = new RestClient(config.Value.BaseApiPath);
                var request = new RestRequest(config.Value.GetInstituteStuctureRequest, Method.GET);
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var content = JsonConvert.DeserializeObject<List<FacultyExternalModel>>(response.Content);

                    if (content == null)
                        return;

                    var mappedData = content.Select(f => new Faculty
                    {
                        Code = f.FacultyCode.ToString(),
                        TitleEn = f.Title_EN,
                        TitleLv = f.Title_LV,
                        TitleRu = f.Title_RU,
                        ShortTitleEn = f.Short_title_EN,
                        ShortTitleLv = f.Short_title_LV,
                        ShortTitleRu = f.Short_title_RU,
                        StudyProgrammes = f.Studyprogrammes.Select(s => new StudyProgramme
                        {
                            Code = s.ProgrammeID.ToString(),
                            TitleEn = s.Title_EN,
                            TitleLv = s.Title_LV,
                            TitleRu = s.Title_RU
                        }).AsQueryable()
                    });

                    var dataFromDb = database.FacultyService.GetAll().Select(f => new {
                        Id = f.Id,
                        Code = f.Code,
                        StudyProgrammes = f.StudyProgrammes.ToList()
                    }).ToList();

                    foreach (var faculty in mappedData)
                    {
                        var facultyFromDb = dataFromDb.FirstOrDefault(f => f.Code == faculty.Code);
                        if (facultyFromDb != null)
                        {
                            var sp = faculty.StudyProgrammes.ToList();
                            sp.ForEach(s =>
                            {
                                var studyProgrammeFromDb = facultyFromDb.StudyProgrammes.FirstOrDefault(sp => sp.Code == s.Code);
                                s.Id = studyProgrammeFromDb?.Id ?? 0;
                            });
                            faculty.Id = facultyFromDb.Id;
                            faculty.StudyProgrammes = sp.AsQueryable();
                            database.FacultyService.Update(faculty);
                        }
                        else
                            database.FacultyService.Add(faculty);
                    }

                    logger.LogInformation("Faculties imported");
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }
        private async Task RequestStudentAtteandance(IDatabase database)
        {
            try
            {
                var client = new RestClient(config.Value.BaseApiPath);
                var request = new RestRequest(config.Value.GetAttendanceRequest, Method.GET);
                var today = DateTime.Today;
                request.AddParameter("date", today.ToString("dd.MM.yyyy"));
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var content = JsonConvert.DeserializeObject<List<StudentAttendanceExternalModel>>(response.Content);

                    if (content == null)
                        return;

                    var studentsFromDb = database.StudentService.GetAll().Select(s => new {
                        Id = s.Id,
                        Code = s.Code
                    });
                    var subjectsFromDb = database.SubjectService.GetAll();

                    foreach (var studentAttendance in content)
                    {
                        var student = studentsFromDb.FirstOrDefault(s => s.Code == studentAttendance.Stcode.ToString());
                        int studentId;

                        if (student == null)
                            studentId = await AddStudent(database, studentAttendance.Stcode);
                        else
                            studentId = student.Id;

                        studentAttendance.Attandance.ForEach(attendance => {

                            var subject = subjectsFromDb.FirstOrDefault(s => s.Code == attendance.Courseid.ToString());

                            if (subject != null)
                            {
                                // Try add attendance if able (if already not)
                                try
                                {
                                    database.StudentAttendanceService.Add(new StudentAttendance
                                    {
                                        Date = today,
                                        StudentId = studentId,
                                        SubjectId = subject.Id,
                                        NecessaryAttendance = attendance.All,
                                        RealAttendance = attendance.Att,
                                    });
                                }
                                catch (Exception exception)
                                {
                                    logger.LogError(exception, exception.Message);
                                }
                            }

                        });
                    }

                    logger.LogInformation("Student attendance imported");
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }
        private async Task<int> AddStudent(IDatabase database, int studentCode)
        {

            try
            {
                var client = new RestClient(config.Value.BaseApiPath);
                var request = new RestRequest(config.Value.GetStudentRequest, Method.GET);
                request.AddParameter("code", studentCode.ToString());
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var student = JsonConvert.DeserializeObject<StudentExternalModel>(response.Content);

                    if (student == null)
                        return 0;

                    var studyProgrammes = database.FacultyService.GetAll().SelectMany(f => f.StudyProgrammes);
                    var studentStudyProgramme = studyProgrammes.FirstOrDefault(s => s.Code == student.Programme.ToString());

                    var mappedStudent = new Student
                    {
                        Id = 0,
                        Code = student.StudentID.ToString(),
                        Name = student.Name,
                        Surname = student.Surname,
                        StudyProgrammeId = studentStudyProgramme.Id,
                        Language = MapLanguage(student.Lng),
                        LearningForm = MapLearningForm(student.Form),
                        Status = MapStatus(student.Status),
                        Phone1 = student.Phones.FirstOrDefault()?.Phone ?? "",
                        Phone2 = student.Phones.Count > 1 ? student.Phones[1].Phone : "",
                        Email1 = student.Emails.FirstOrDefault()?.Email ?? "",
                        Email2 = student.Emails.Count > 1 ? student.Emails[1].Email : "",
                        Skype = student.Skype,
                        Comment = student.Comments
                    };

                    logger.LogInformation("Student imported");
                    return database.StudentService.Add(mappedStudent).Id;
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
            return 0;
        }

        private SudentLanguage MapLanguage(string language)
        {
            switch (language)
            {
                case "latvian":
                    return SudentLanguage.Latvian;
                case "russian":
                    return SudentLanguage.Russian;
                case "english":
                default:
                    return SudentLanguage.English;
            }
        }
        private LearningForm MapLearningForm(int learningForm)
        {
            switch (learningForm)
            {
                case 2:
                    return LearningForm.Evenings;
                case 3:
                    return LearningForm.Weekends;
                case 4:
                    return LearningForm.Distance;
                case 1:
                default:
                    return LearningForm.Daytime;
            }
        }
        private StudentStatus MapStatus(string status)
        {
            switch (status)
            {
                case "inactive":
                    return StudentStatus.Inactive;
                case "active":
                default:
                    return StudentStatus.Active;
            }
        }
    }
}
