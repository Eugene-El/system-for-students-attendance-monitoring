using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.Attendance.Receiving.Service.ExternalModels
{
    public class StudentAttendanceExternalModel
    {
        public int Stcode { get; set; }
        public List<AttendanceExternalModel> Attandance { get; set; }
    }
}
