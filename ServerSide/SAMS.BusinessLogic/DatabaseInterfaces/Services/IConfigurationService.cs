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
    }
}
