using SAMS.BusinessLogic.Models.StudentAttendances.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.DataModels
{
    public class HomeDataModel
    {
        public int CurrentOnline { get; set; }
        public List<FacultyAttendanceModel> FacultyStatistics { get; set; }
    }
}
