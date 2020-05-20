using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.TTI.REST.API.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Programme { get; set; }
        public int Faculty { get; set; }
        public int Form { get; set; }
        public string Lng { get; set; }
        public List<PhoneC> Phones { get; set; }
        public List<EmailC> Emails { get; set; }
        public string Skype { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }

    }
}
