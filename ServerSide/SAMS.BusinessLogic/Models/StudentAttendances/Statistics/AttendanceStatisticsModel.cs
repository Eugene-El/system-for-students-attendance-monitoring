using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.StudentAttendances.Statistics
{
    public class AttendanceStatisticsModel
    {
        public AttendanceStatisticsModel()
        {
            SubjectStatistics = new List<SubjectStatisticsModel>();
            GlobalStatistics = new List<DayStatisticsModel>();
        }

        public int GlobalAttendanceProcent { get; set; }

        public List<DayStatisticsModel> GlobalStatistics { get; set; }

        public List<SubjectStatisticsModel> SubjectStatistics { get; set; }
    }
}
