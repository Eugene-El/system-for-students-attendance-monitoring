using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Entities
{
    public class StudentAttendance
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public DateTime Date { get; set; }

        public int NecessaryAttendance { get; set; }
        public int RealAttendance { get; set; }
    }
}
