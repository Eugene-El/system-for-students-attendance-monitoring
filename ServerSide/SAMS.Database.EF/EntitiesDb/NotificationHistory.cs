using SAMS.BusinessLogic.Entities.Enumerations;
using System;

namespace SAMS.Database.EF.EntitiesDb
{
    public class NotificationHistory
    {
        public int Id { get; set; }
        public DateTime SendingTime { get; set; }
        public string Message { get; set; }
        public NotificationStatus Status { get; set; }
        public string ErrorMessage { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }


        public BusinessLogic.Entities.NotificationHistory MapToEntity()
        {
            return new BusinessLogic.Entities.NotificationHistory
            {
                Id = this.Id,
                SendingTime = this.SendingTime,
                Message = this.Message,
                Status = this.Status,
                ErrorMessage = this.ErrorMessage,
                StudentId = this.StudentId
            };
        }

        public NotificationHistory MapFromEntity(BusinessLogic.Entities.NotificationHistory notificationHistory)
        {
            Id = notificationHistory.Id;
            SendingTime = notificationHistory.SendingTime;
            Message = notificationHistory.Message;
            Status = notificationHistory.Status;
            ErrorMessage = notificationHistory.ErrorMessage;
            StudentId = notificationHistory.StudentId;
            return this;
        }
    }
}
