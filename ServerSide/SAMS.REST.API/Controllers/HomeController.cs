using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.Controllers
{
    public class HomeController : MainController
    {
        [HttpGet]
        public string Test()
        {
            return "Successfull test";
        }
    }
}
