using SAMS.BusinessLogic.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.Students
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int StudyProgrammeId { get; set; }

        public int Language { get; set; }
        public int LearningForm { get; set; }
        public int Status { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Skype { get; set; }
        public string Comment { get; set; }
    }
}
