using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.NotificationHistories
{
    public class NotificationHistoryModel
    {
        public int Id { get; set; }
        public string SendingTime { get; set; }
        public string FullName { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
    }
}
