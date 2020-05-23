using Microsoft.AspNetCore.Mvc;
using SAMS.BusinessLogic.Models.Students;
using SAMS.Database.EF.EntityFramework;
using SAMS.REST.API.Authorization;
using SAMS.REST.API.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.Controllers
{
    public class StudentsController : MainController
    {
        public StudentsController(DataContext dataContext) : base(dataContext) { }

        [HttpGet]
        public IActionResult GetAllForGrid()
        {
            try
            {
                CheckAuthorization(Role.Worker, Role.Lecturer);
                return Ok(FactoryConcentrator.StudentFacotry.GetAllForGrid(CurrentLanguage).AsEnumerable());
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
                CheckAuthorization(Role.Worker);
                return Ok(new StudentDataModel {
                    Student = FactoryConcentrator.StudentFacotry.Get(id),
                    Faculties = FactoryConcentrator.FacultyFactory.GetAllSelectModel(CurrentLanguage)
                });
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost]
        public IActionResult Save(StudentModel studentModel)
        {
            try
            {
                CheckAuthorization(Role.Worker);
                if (studentModel != null)
                {
                    if (studentModel.Id == 0)
                        FactoryConcentrator.StudentFacotry.Add(studentModel);
                    else
                        FactoryConcentrator.StudentFacotry.Update(studentModel);
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
