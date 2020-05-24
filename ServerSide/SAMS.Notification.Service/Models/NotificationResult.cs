using SAMS.BusinessLogic.Entities.Enumerations;

namespace SAMS.Notification.Service.Models
{
    public class NotificationResult
    {
        public NotificationStatus NotificationStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}
