using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.Students
{
    public class StudentGridModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string FacultyTitle { get; set; }
        public string StudyProgrammeTitle { get; set; }
        public int LearningForm { get; set; }
        public int StudentLanguage { get; set; }
    }
}
