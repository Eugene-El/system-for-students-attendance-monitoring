using SAMS.BusinessLogic.Models.StudentAttendances;
using SAMS.BusinessLogic.Models.Students;
using System.Collections.Generic;

namespace SAMS.REST.API.DataModels
{
    public class StudentAttendanceDataModel
    {
        public StudentShortModel Student { get; set; }
        public IEnumerable<StudentAttandanceGridModel> StudentAttandances { get; set; }
    }
}
