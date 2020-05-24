using SAMS.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.DatabaseInterfaces.Services
{
    public interface IConfigurationService
    {
        public NotificationAndSyncConfiguration GetNotificationAndSyncConfiguration();
        public void SetNotificationAndSyncConfiguration(NotificationAndSyncConfiguration configuration);

        public DateTime? GetLastDataSyncTime();
        public void SetLastDataSyncTime(DateTime dateTime);

        public DateTime? GetLastNotificationTime();
        public void SetLastNotificationTime(DateTime dateTime);
    }
}
