using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.Configurations
{
    public class NotificationAndSyncConfigurationModel
    {
        public int DataSyncPeriodHours { get; set; }
        public int NotificationsPeriodHours { get; set; }
    }
}
