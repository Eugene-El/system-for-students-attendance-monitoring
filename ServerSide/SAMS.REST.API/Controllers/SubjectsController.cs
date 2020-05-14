using Microsoft.AspNetCore.Mvc;
using SAMS.BusinessLogic.Models.Subjects;
using SAMS.Database.EF.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.Controllers
{
    public class SubjectsController : MainController
    {
        public SubjectsController(DataContext dataContext) : base(dataContext) { }

        [HttpGet]
        public IActionResult GetAllForGrid()
        {
            try
            {
                return Ok(FactoryConcentrator.SubjectFactory.GetAllForGrid(CurrentLanguage).AsEnumerable());
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
                return Ok(FactoryConcentrator.SubjectFactory.Get(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost]
        public IActionResult Save(SubjectModel subjectModel)
        {
            try
            {
                if (subjectModel != null)
                {
                    if (subjectModel.Id == 0)
                        FactoryConcentrator.SubjectFactory.Add(subjectModel);
                    else
                        FactoryConcentrator.SubjectFactory.Update(subjectModel);
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
