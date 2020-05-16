using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.StudentAttendances
{
    public class StudentAttandanceGridModel
    {
        public int Id { get; set; }
        public string SubjectTitle { get; set; }
        public string Date { get; set; }

        public int NecessaryAttendance { get; set; }
        public int RealAttendance { get; set; }
    }
}
