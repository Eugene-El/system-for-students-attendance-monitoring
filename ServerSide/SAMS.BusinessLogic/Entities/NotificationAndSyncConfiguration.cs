using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Entities
{
    public class NotificationAndSyncConfiguration
    {
        public int DataSyncPeriodHours { get; set; }
        public int NotificationsPeriodHours { get; set; }
    }
}
