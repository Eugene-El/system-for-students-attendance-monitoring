using Microsoft.AspNetCore.Mvc;
using SAMS.Database.EF.EntityFramework;
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
        public string Test()
        {
            return "Successfull test";
        }
    }
}
