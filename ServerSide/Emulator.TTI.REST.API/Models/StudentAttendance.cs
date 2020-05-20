using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.TTI.REST.API.Models
{
    public class StudentAttendance
    {
        public int Stcode { get; set; }
        public List<Attendance> Attandance { get; set; }
    }
}
