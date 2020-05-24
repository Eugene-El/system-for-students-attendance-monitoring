using SAMS.BusinessLogic.Entities;
using System.Linq;

namespace SAMS.BusinessLogic.DatabaseInterfaces.Services
{
    public interface INotificationHistoryService
    {
        IQueryable<NotificationHistory> GetAll();
        NotificationHistory Add(NotificationHistory notificationHistory);
    }
}
