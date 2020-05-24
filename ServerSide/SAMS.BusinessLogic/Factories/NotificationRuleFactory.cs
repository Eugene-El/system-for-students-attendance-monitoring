using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Entities;
using System.Linq;

namespace SAMS.BusinessLogic.Factories
{
    public class NotificationRuleFactory : MainFactory
    {
        protected IDatabase Database { get; private set; }
        public NotificationRuleFactory(IDatabase database)
        {
            Database = database;
        }

        public IQueryable<NotificationRule> GetAll()
        {
            return Database.NotificationRuleService.GetAll();
        }

        public void Add(NotificationRule notificationRule)
        {
            Database.NotificationRuleService.Add(notificationRule);
        }

        public void Update(NotificationRule notificationRule)
        {
            Database.NotificationRuleService.Update(notificationRule);
        }

        public void Delete(int id)
        {
            Database.NotificationRuleService.Delete(id);
        }
    }
}
