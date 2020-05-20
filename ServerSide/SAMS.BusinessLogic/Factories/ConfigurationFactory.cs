using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Models.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Factories
{
    public class ConfigurationFactory : MainFactory
    {
        protected IDatabase Database { get; private set; }
        public ConfigurationFactory(IDatabase database)
        {
            Database = database;
        }

        public ConfigurationModel Get()
        {
            var notificationAndSyncConfiguration = Database.ConfigurationService.GetNotificationAndSyncConfiguration();

            return new ConfigurationModel
            {
                NotificationAndSyncConfiguration = new NotificationAndSyncConfigurationModel
                {
                    DataSyncPeriodHours = notificationAndSyncConfiguration.DataSyncPeriodHours,
                    NotificationsPeriodHours = notificationAndSyncConfiguration.NotificationsPeriodHours
                }
            };
        }

        public void Save(ConfigurationModel configurationModel)
        {
            if (configurationModel != null)
            {
                if (configurationModel.NotificationAndSyncConfiguration != null)
                    Database.ConfigurationService.SetNotificationAndSyncConfiguration(new Entities.NotificationAndSyncConfiguration
                    {
                        DataSyncPeriodHours = configurationModel.NotificationAndSyncConfiguration.DataSyncPeriodHours,
                        NotificationsPeriodHours = configurationModel.NotificationAndSyncConfiguration.NotificationsPeriodHours
                    });
            }
        }

        public DateTime? GetLastDataSyncTime()
        {
            return Database.ConfigurationService.GetLastDataSyncTime();
        }

        public void SetLastDataSyncTime(DateTime dateTime)
        {
            Database.ConfigurationService.SetLastDataSyncTime(dateTime);
        }
    }
}
