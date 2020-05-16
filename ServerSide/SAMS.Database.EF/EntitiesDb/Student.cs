using SAMS.BusinessLogic.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMS.Database.EF.EntitiesDb
{
    public class Student
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int StudyProgrammeId { get; set; }
        public StudyProgramme StudyProgramme { get; set; }

        public SudentLanguage Language { get; set; }
        public LearningForm LearningForm { get; set; }
        public StudentStatus Status { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Skype { get; set; }
        public string Comment { get; set; }

        public ICollection<StudentAttendance> StudentAttendances { get; set; }

        public BusinessLogic.Entities.Student MapToEntity()
        {

            return new BusinessLogic.Entities.Student
            {
                Id = this.Id,
                Code = this.Code,
                Name = this.Name,
                Surname = this.Surname,
                StudyProgrammeId = this.StudyProgrammeId,
                Language = this.Language,
                LearningForm = this.LearningForm,
                Status = this.Status,
                Phone1 = this.Phone1,
                Phone2 = this.Phone2,
                Email1 = this.Email1,
                Email2 = this.Email2,
                Skype = this.Skype,
                Comment = this.Comment,
                StudentAttendances = this.StudentAttendances?.Select(s => s.MapToEntity()).AsQueryable()
            };
        }

        public Student MapFromEntity(BusinessLogic.Entities.Student student)
        {
            Id = student.Id;
            Code = student.Code;
            Name = student.Name;
            Surname = student.Surname;
            StudyProgrammeId = student.StudyProgrammeId;
            Language = student.Language;
            LearningForm = student.LearningForm;
            Status = student.Status;
            Phone1 = student.Phone1;
            Phone2 = student.Phone2;
            Email1 = student.Email1;
            Email2 = student.Email2;
            Skype = student.Skype;
            Comment = student.Comment;
            StudentAttendances = student.StudentAttendances?.Select(s => new StudentAttendance().MapFromEntity(s)).ToList() ?? new List<StudentAttendance>();
            return this;
        }

    }
}
