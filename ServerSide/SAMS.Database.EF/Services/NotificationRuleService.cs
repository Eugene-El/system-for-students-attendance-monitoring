using SAMS.BusinessLogic.DatabaseInterfaces.Services;
using SAMS.BusinessLogic.Entities;
using SAMS.Database.EF.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMS.Database.EF.Services
{
    public class NotificationRuleService : INotificationRuleService
    {
        private DataContext dataContext;
        public NotificationRuleService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<NotificationRule> GetAll()
        {
            return dataContext.NotificationRules
                .Select(n => new NotificationRule
                {
                    Id = n.Id,
                    Name = n.Name,
                    NotificationMethod = n.NotificationMethod,
                    StudyProgrammeId = n.StudyProgrammeId,
                    Language = n.Language,
                    LearningForm = n.LearningForm,
                    AttendancePeriod = n.AttendancePeriod,
                    AttendanceProcent = n.AttendanceProcent,
                    Message = n.Message
                });
        }

        public NotificationRule Add(NotificationRule notificationRule)
        {
            var notificationRuleToDb = new EntitiesDb.NotificationRule().MapFromEntity(notificationRule);
            var notificationRuleFromDb = dataContext.Add(notificationRuleToDb).Entity;
            dataContext.SaveChanges();
            return notificationRuleFromDb.MapToEntity();
        }

        public NotificationRule Update(NotificationRule notificationRule)
        {
            var notificationRuleToDb = new EntitiesDb.NotificationRule().MapFromEntity(notificationRule);
            var notificationRuleFromDb = dataContext.Update(notificationRuleToDb).Entity;
            dataContext.SaveChanges();
            return notificationRuleFromDb.MapToEntity();
        }

        public void Delete(int id)
        {
            var notificationRule = dataContext.NotificationRules.Find(id);
            if (notificationRule != null)
                dataContext.NotificationRules.Remove(notificationRule);

            dataContext.SaveChanges();
        }
    }
}
