using Microsoft.AspNetCore.Mvc;
using SAMS.Database.EF.EntityFramework;
using SAMS.REST.API.Authorization;
using SAMS.REST.API.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.Controllers
{
    public class OwnAttendanceController : MainController
    {
        public OwnAttendanceController(DataContext dataContext) : base(dataContext) { }

        [HttpPost]
        public IActionResult GetAttendanceStatistics(AttendanceFilterModel filter)
        {
            try
            {
                CheckAuthorization(Role.Student);

                var stLogin = CurrentUser.Login.ToLower();
                var stCode = stLogin.StartsWith("st") ? stLogin.Substring(2) : stLogin;
                return Ok(FactoryConcentrator.StudentAttendanceFactory.GetStatistics(
                    CurrentLanguage,
                    FactoryConcentrator.StudentFacotry.GetStudentIdByCode(stCode),
                    filter.From,
                    filter.To));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
