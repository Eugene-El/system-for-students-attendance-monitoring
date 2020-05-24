using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Entities;
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

        public IQueryable<NotificationHistory> GetAll()
        {
            return Database.NotificationHistoryService.GetAll();
        }

        public void Add(NotificationHistory notificationHistory)
        {
            Database.NotificationHistoryService.Add(notificationHistory);
        }

    }
}
