using Newtonsoft.Json;
using SAMS.BusinessLogic.DatabaseInterfaces.Services;
using SAMS.BusinessLogic.Entities;
using SAMS.Database.EF.EntityFramework;
using System;
using System.Linq;

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
            var configurationObject = dataContext.Configurations.FirstOrDefault(c =>
                c.Type == EntitiesDb.ConfigurationType.NotificationAndSyncTimePeriods);
            if (configurationObject == null)
                configurationObject = new EntitiesDb.Configuration
                {
                    Id = 0,
                    Type = EntitiesDb.ConfigurationType.NotificationAndSyncTimePeriods
                };

            configurationObject.Content = JsonConvert.SerializeObject(configuration);

            if (configurationObject.Id == 0)
                dataContext.Configurations.Add(configurationObject);
            else
                dataContext.Configurations.Update(configurationObject);

            dataContext.SaveChanges();
        }

        public DateTime? GetLastDataSyncTime()
        {
            return GetTime(EntitiesDb.ConfigurationType.LastDataSyncTime);
        }

        public void SetLastDataSyncTime(DateTime dateTime)
        {
            SetTime(EntitiesDb.ConfigurationType.LastDataSyncTime, dateTime);
        }

        public DateTime? GetLastNotificationTime()
        {
            return GetTime(EntitiesDb.ConfigurationType.LastNotificationTime);
        }

        public void SetLastNotificationTime(DateTime dateTime)
        {
            SetTime(EntitiesDb.ConfigurationType.LastNotificationTime, dateTime);
        }

        private DateTime? GetTime(EntitiesDb.ConfigurationType configurationType)
        {
            var configurationString = dataContext.Configurations.FirstOrDefault(c =>
                c.Type == configurationType)?.Content;

            if (string.IsNullOrEmpty(configurationString))
                return null;

            return JsonConvert.DeserializeObject<DateTime>(configurationString);
        }

        private void SetTime(EntitiesDb.ConfigurationType configurationType, DateTime dateTime)
        {
            var configurationObject = dataContext.Configurations.FirstOrDefault(c =>
                c.Type == configurationType);

            if (configurationObject == null)
                configurationObject = new EntitiesDb.Configuration
                {
                    Id = 0,
                    Type = configurationType
                };

            configurationObject.Content = JsonConvert.SerializeObject(dateTime);

            if (configurationObject.Id == 0)
                dataContext.Configurations.Add(configurationObject);
            else
                dataContext.Configurations.Update(configurationObject);

            dataContext.SaveChanges();
        }
    }

}
