using SAMS.BusinessLogic.Entities;
using System.Linq;

namespace SAMS.BusinessLogic.DatabaseInterfaces.Services
{
    public interface INotificationRuleService
    {
        IQueryable<NotificationRule> GetAll();
        NotificationRule Add(NotificationRule notificationRule);
        NotificationRule Update(NotificationRule notificationRule);
        void Delete(int id);
    }
}
