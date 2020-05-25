using Microsoft.AspNetCore.Mvc;
using SAMS.Database.EF.EntityFramework;
using SAMS.REST.API.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.Controllers
{
    public class HomeController : MainController
    {
        public HomeController(DataContext dataContext) : base(dataContext) { }

        [HttpGet]
        public IActionResult GetFacultyStatistics()
        {
            try
            {                
                return Ok(new HomeDataModel {
                    CurrentOnline = AuthorizedUsers.Count(),
                    FacultyStatistics = FactoryConcentrator.StudentAttendanceFactory.GetStatisticsByFaculties(
                    CurrentLanguage,
                    DateTime.Today.AddDays(-30),
                    DateTime.Today)
                });
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
