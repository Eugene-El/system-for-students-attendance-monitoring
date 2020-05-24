using SAMS.BusinessLogic.DatabaseInterfaces.Services;
using SAMS.BusinessLogic.Entities;
using SAMS.Database.EF.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMS.Database.EF.Services
{
    public class NotificationHistoryService : INotificationHistoryService
    {

        private DataContext dataContext;
        public NotificationHistoryService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<NotificationHistory> GetAll()
        {
            return dataContext.NotificationHistories
                .Select(n => new NotificationHistory
                {
                    Id = n.Id,
                    Message = n.Message,
                    SendingTime = n.SendingTime,
                    Status = n.Status,
                    ErrorMessage = n.ErrorMessage,
                    StudentId = n.StudentId
                });
        }

        public NotificationHistory Add(NotificationHistory notificationHistory)
        {
            var notificationHistoryToDb = new EntitiesDb.NotificationHistory().MapFromEntity(notificationHistory);
            var notificationHistoryFromDb = dataContext.Add(notificationHistoryToDb).Entity;
            dataContext.SaveChanges();
            return notificationHistoryFromDb.MapToEntity();
        }
    }
}
