using SAMS.BusinessLogic.Entities;
using SAMS.BusinessLogic.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.DataModels
{
    public class NotificationRuleDataModel
    {
        public IQueryable<NotificationRule> NotificationRules{ get; set; }
        public IQueryable<ExtraSelectModel> Faculties { get; set; }
    }
}
