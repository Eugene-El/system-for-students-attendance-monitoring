using Microsoft.AspNetCore.Mvc;
using SAMS.BusinessLogic.Models.Common;
using SAMS.BusinessLogic.Models.Faculty;
using SAMS.Database.EF.EntityFramework;
using SAMS.REST.API.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.Controllers
{
    public class FacultiesController : MainController
    {
        public FacultiesController(DataContext dataContext) : base(dataContext) { }

        [HttpGet]
        public IActionResult GetAllForGrid()
        {
            try
            {
                //CheckAuthorization(Role.Worker);
                return Ok(FactoryConcentrator.FacultyFactory.GetAllForGrid(CurrentLanguage).AsEnumerable());
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            try
            {
                //CheckAuthorization(Role.Worker);
                return Ok(FactoryConcentrator.FacultyFactory.Get(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost]
        public IActionResult Save(FacultyModel facultyModel)
        {
            try
            {
                CheckAuthorization(Role.Worker);
                if (facultyModel != null)
                {
                    if (facultyModel.Id == 0)
                        FactoryConcentrator.FacultyFactory.Add(facultyModel);
                    else
                        FactoryConcentrator.FacultyFactory.Update(facultyModel);
                }
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
