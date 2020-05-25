using Microsoft.AspNetCore.Mvc;
using SAMS.Database.EF.EntityFramework;
using SAMS.REST.API.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.Controllers
{
    public class NotificationHistoriesController : MainController
    {
        public NotificationHistoriesController(DataContext dataContext) : base(dataContext) { }


        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                CheckAuthorization(Role.Worker);
                return Ok(FactoryConcentrator.NotificationHistoryFactory.GetAll());
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
