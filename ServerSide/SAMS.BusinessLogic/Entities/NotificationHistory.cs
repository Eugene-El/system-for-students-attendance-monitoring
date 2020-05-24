using SAMS.BusinessLogic.Entities.Enumerations;
using System;

namespace SAMS.BusinessLogic.Entities
{
    public class NotificationHistory
    {
        public int Id { get; set; }
        public DateTime SendingTime { get; set; }
        public string Message { get; set; }
        public NotificationStatus Status { get; set; }
        public string ErrorMessage { get; set; }

        public int StudentId { get; set; }


    }
}
