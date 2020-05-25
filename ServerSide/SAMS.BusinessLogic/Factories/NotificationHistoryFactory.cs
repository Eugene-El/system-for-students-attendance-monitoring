using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Entities;
using SAMS.BusinessLogic.Models.Common;
using SAMS.BusinessLogic.Models.NotificationHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMS.BusinessLogic.Factories
{
    public class NotificationHistoryFactory : MainFactory
    {
        protected IDatabase Database { get; private set; }
        public NotificationHistoryFactory(IDatabase database)
        {
            Database = database;
        }

        public IQueryable<NotificationHistoryModel> GetAll()
        {
            var students = Database.StudentService.GetAll().Select(s => new SelectModel
            {
                Id = s.Id,
                Title = s.Surname + " " + s.Name
            }).ToList();
            return Database.NotificationHistoryService.GetAll().ToList().Select(n => new NotificationHistoryModel {
                Id = n.Id,
                FullName = students.FirstOrDefault(st => st.Id == n.StudentId).Title,
                SendingTime = n.SendingTime.ToString("dd.MM.yyyy hh:mm"),
                Status = (int)n.Status,
                Message = n.Message,
                ErrorMessage = n.ErrorMessage
            }).AsQueryable();
        }

        public void Add(NotificationHistory notificationHistory)
        {
            Database.NotificationHistoryService.Add(notificationHistory);
        }

    }
}
