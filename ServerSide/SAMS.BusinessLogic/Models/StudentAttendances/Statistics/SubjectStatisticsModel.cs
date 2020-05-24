using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.StudentAttendances.Statistics
{
    public class SubjectStatisticsModel
    {
        public SubjectStatisticsModel()
        {
            DayStatistics = new List<DayStatisticsModel>();
        }

        public int SubjectId { get; set; }
        public string Title { get; set; }
        public List<DayStatisticsModel> DayStatistics { get; set; }
    }
}
