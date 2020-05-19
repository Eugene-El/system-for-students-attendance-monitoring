using Newtonsoft.Json;
using SAMS.BusinessLogic.DatabaseInterfaces.Services;
using SAMS.BusinessLogic.Entities;
using SAMS.Database.EF.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMS.Database.EF.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private DataContext dataContext;
        public ConfigurationService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public NotificationAndSyncConfiguration GetNotificationAndSyncConfiguration()
        {
            var configurationString = dataContext.Configurations.FirstOrDefault(c =>
                c.Type == EntitiesDb.ConfigurationType.NotificationAndSyncTimePeriods)?.Content;

            NotificationAndSyncConfiguration configuration;
            if (string.IsNullOrEmpty(configurationString))
                configuration = new NotificationAndSyncConfiguration
                {
                    DataSyncPeriodHours = 24,
                    NotificationsPeriodHours = 24 * 7
                };
            else
                configuration = JsonConvert.DeserializeObject<NotificationAndSyncConfiguration>(configurationString);

            return configuration;
        }

        public void SetNotificationAndSyncConfiguration(NotificationAndSyncConfiguration configuration)
        {
            var configurationString = JsonConvert.SerializeObject(configuration);

            var configurationObject = dataContext.Configurations.FirstOrDefault(c =>
                c.Type == EntitiesDb.ConfigurationType.NotificationAndSyncTimePeriods);
            if (configurationObject == null)
                configurationObject = new EntitiesDb.Configuration
                {
                    Id = 0,
                    Type = EntitiesDb.ConfigurationType.NotificationAndSyncTimePeriods
                };

            configurationObject.Content = configurationString;

            if (configurationObject.Id == 0)
                dataContext.Configurations.Add(configurationObject);
            else
                dataContext.Configurations.Update(configurationObject);

            dataContext.SaveChanges();
        }
    }
}
