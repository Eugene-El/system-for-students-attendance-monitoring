using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.Faculty
{
    public class FacultyGridModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int StudyProgrammeCount { get; set; }
    }
}
