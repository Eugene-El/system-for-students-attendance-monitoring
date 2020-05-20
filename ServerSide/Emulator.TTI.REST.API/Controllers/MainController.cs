using Emulator.TTI.REST.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Emulator.TTI.REST.API.Controllers
{
    [ApiController]
    [Route("api/[action]/")]
    public class MainController : ControllerBase
    {
        protected T ReadDataFromFile<T>(string pathToFile)
        {
            if (System.IO.File.Exists(pathToFile))
            {
                var myJsonString = System.IO.File.ReadAllText(pathToFile);
                T data = JsonConvert.DeserializeObject<T>(myJsonString);
                return data;
            }
            throw new Exception("No data found");
        }

        [HttpGet]
        public IActionResult GetSubjects()
        {
            try
            {
                return Ok(ReadDataFromFile<List<Subject>>("Data/subjects.json"));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }


        [HttpGet]
        public IActionResult GetInstituteStructure()
        {
            try
            {
                return Ok(ReadDataFromFile<List<Faculty>>("Data/faculties.json"));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpGet]
        public IActionResult GetStudent(string code)
        {
            try
            {
                return Ok(ReadDataFromFile<List<Student>>("Data/students.json")
                    .FirstOrDefault(f => f.StudentID.ToString() == code));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpGet]
        public IActionResult GetAttendance(string date)
        {
            try
            {
                return Ok(ReadDataFromFile<List<StudentAttendance>>(
                    string.Format("Data/studentAttendance_{0}.json", date)));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
