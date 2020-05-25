using SAMS.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMS.BusinessLogic.DatabaseInterfaces.Services
{
    public interface IStudentAttendanceService
    {
        IQueryable<StudentAttendance> GetAll();
        IQueryable<StudentAttendance> GetAllByStudentId(int id);
        StudentAttendance Get(int id);
        StudentAttendance Add(StudentAttendance studentAttendance);
        StudentAttendance Update(StudentAttendance studentAttendance);
    }
}
