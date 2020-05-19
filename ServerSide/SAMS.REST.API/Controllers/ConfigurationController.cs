﻿using Microsoft.AspNetCore.Mvc;
using SAMS.BusinessLogic.Models.Configurations;
using SAMS.Database.EF.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.Controllers
{
    public class ConfigurationController : MainController
    {
        public ConfigurationController(DataContext dataContext) : base(dataContext) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(FactoryConcentrator.ConfigurationFactory.Get());
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }


        [HttpPost]
        public IActionResult Save(ConfigurationModel configurationModel)
        {
            try
            {
                FactoryConcentrator.ConfigurationFactory.Save(configurationModel);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

    }
}
