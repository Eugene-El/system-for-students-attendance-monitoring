using Microsoft.AspNetCore.Mvc;
using SAMS.BusinessLogic.Models.StudentAttendances;
using SAMS.Database.EF.EntityFramework;
using SAMS.REST.API.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.Controllers
{
    public class StudentAttendancesController : MainController
    {
        public StudentAttendancesController(DataContext dataContext) : base(dataContext) { }

        [HttpGet]
        public IActionResult GetAllForGrid(int id)
        {
            try
            {
                return Ok(new StudentAttendanceDataModel {
                    Student = FactoryConcentrator.StudentFacotry.GetShortModel(CurrentLanguage, id),
                    StudentAttandances = FactoryConcentrator.StudentAttendanceFactory.GetAllForGrid(CurrentLanguage, id).AsEnumerable()
                });
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
                return Ok(new StudentAttendanceEditDataModel
                {
                    StudentAttendance = FactoryConcentrator.StudentAttendanceFactory.Get(id),
                    Subjects = FactoryConcentrator.SubjectFactory.GetAllSelectModel(CurrentLanguage)
                });
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost]
        public IActionResult Save(StudentAttendanceModel studentAttendanceModel)
        {
            try
            {
                if (studentAttendanceModel != null)
                {
                    if (studentAttendanceModel.Id == 0)
                        FactoryConcentrator.StudentAttendanceFactory.Add(studentAttendanceModel);
                    else
                        FactoryConcentrator.StudentAttendanceFactory.Update(studentAttendanceModel);
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
