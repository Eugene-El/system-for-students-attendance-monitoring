using Microsoft.AspNetCore.Mvc;
using SAMS.BusinessLogic.Entities;
using SAMS.Database.EF.EntityFramework;
using SAMS.REST.API.Authorization;
using SAMS.REST.API.DataModels;
using System;

namespace SAMS.REST.API.Controllers
{
    public class NotificationRulesController : MainController
    {

        public NotificationRulesController(DataContext dataContext) : base(dataContext) { }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                CheckAuthorization(Role.Worker);
                return Ok(new NotificationRuleDataModel {
                    NotificationRules = FactoryConcentrator.NotificationRuleFactory.GetAll(),
                    Faculties = FactoryConcentrator.FacultyFactory.GetAllSelectModel(CurrentLanguage)
                });
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }


        [HttpPost]
        public IActionResult Save(NotificationRule notificationRule)
        {
            try
            {
                CheckAuthorization(Role.Worker);

                if (notificationRule != null)
                {
                    if (notificationRule.Id == 0)
                        FactoryConcentrator.NotificationRuleFactory.Add(notificationRule);
                    else
                        FactoryConcentrator.NotificationRuleFactory.Update(notificationRule);
                }
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                CheckAuthorization(Role.Worker);

                FactoryConcentrator.NotificationRuleFactory.Delete(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
