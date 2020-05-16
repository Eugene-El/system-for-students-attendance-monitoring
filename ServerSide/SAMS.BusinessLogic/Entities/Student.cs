using SAMS.BusinessLogic.Entities.Enumerations;
using System.Linq;

namespace SAMS.BusinessLogic.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int StudyProgrammeId { get; set; }

        public SudentLanguage Language { get; set; }
        public LearningForm LearningForm { get; set; }
        public StudentStatus Status { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Skype { get; set; }
        public string Comment { get; set; }

        public IQueryable<StudentAttendance> StudentAttendances { get; set; }
    }
}
