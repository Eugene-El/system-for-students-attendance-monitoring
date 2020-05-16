using SAMS.BusinessLogic.Models.Common;
using SAMS.BusinessLogic.Models.StudentAttendances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.DataModels
{
    public class StudentAttendanceEditDataModel
    {
        public StudentAttendanceModel StudentAttendance { get; set; }
        public IEnumerable<SelectModel> Subjects { get; set; }
    }
}
