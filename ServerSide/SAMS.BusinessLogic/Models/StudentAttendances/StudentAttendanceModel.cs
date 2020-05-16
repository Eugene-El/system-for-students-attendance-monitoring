using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.StudentAttendances
{
    public class StudentAttendanceModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }

        public int NecessaryAttendance { get; set; }
        public int RealAttendance { get; set; }
    }
}
