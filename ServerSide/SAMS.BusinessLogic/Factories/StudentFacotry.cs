﻿using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Entities.Enumerations;
using SAMS.BusinessLogic.Models.Common;
using SAMS.BusinessLogic.Models.Students;
using System.Linq;

namespace SAMS.BusinessLogic.Factories
{
    public class StudentFacotry : MainFactory
    {
        protected IDatabase Database { get; private set; }
        public StudentFacotry(IDatabase database)
        {
            Database = database;
        }

        public IQueryable<StudentGridModel> GetAllForGrid(Language language)
        {
            var faculties = Database.FacultyService.GetAll();
            var studyProgrammes = faculties.SelectMany(f => f.StudyProgrammes);
            return Database.StudentService.GetAll()
                .Select(s => new {
                    Id = s.Id,
                    Code = s.Code,
                    FullName = s.Surname + " " + s.Name,
                    StudyProgramme = studyProgrammes.FirstOrDefault(sp => sp.Id == s.StudyProgrammeId),
                    Faculty = faculties.FirstOrDefault(f => f.StudyProgrammes.Any(sp => sp.Id == s.StudyProgrammeId)),
                    LearningForm = s.LearningForm,
                    StudentLanguage = s.Language
                })
                .Select(s => new StudentGridModel
                {
                    Id = s.Id,
                    Code = s.Code,
                    FullName = s.FullName,
                    StudyProgrammeTitle = SelectLocalization(language, s.StudyProgramme.TitleEn, s.StudyProgramme.TitleLv, s.StudyProgramme.TitleRu),
                    FacultyTitle = SelectLocalization(language, s.Faculty.ShortTitleEn, s.Faculty.ShortTitleLv, s.Faculty.ShortTitleRu),
                    LearningForm = (int)s.LearningForm,
                    StudentLanguage = (int)s.StudentLanguage
                });
        }

        public int GetStudentIdByCode(string code)
        {
            return Database.StudentService.GetAll()
                .Select(s => new {
                    Id = s.Id,
                    Code = s.Code
                })
                .FirstOrDefault(s => s.Code == code)?.Id ?? 0;
        }

        public StudentModel Get(int id)
        {
            var student = Database.StudentService.Get(id);
            return student == null ?
                null : new StudentModel
                {
                    Id = student.Id,
                    Code = student.Code,
                    Name = student.Name,
                    Surname = student.Surname,
                    StudyProgrammeId = student.StudyProgrammeId,
                    Language = (int)student.Language,
                    LearningForm = (int)student.LearningForm,
                    Status = (int)student.Status,
                    Phone1 = student.Phone1,
                    Phone2 = student.Phone2,
                    Email1 = student.Email1,
                    Email2 = student.Email2,
                    Skype = student.Skype,
                    Comment = student.Comment
                };
        }

        public StudentShortModel GetShortModel(Language language, int id)
        {
            var faculties = Database.FacultyService.GetAll();
            var studyProgrammes = faculties.SelectMany(f => f.StudyProgrammes);
            var student = Database.StudentService.Get(id);
            var studyProgramme = studyProgrammes.FirstOrDefault(s => s.Id == student.StudyProgrammeId);
            return student == null ?
                null : new StudentShortModel
                {
                    Id = student.Id,
                    Code = student.Code,
                    FullName = student.Surname + " " + student.Name,
                    StudyProgrammeTitle = studyProgramme != null ?
                        SelectLocalization(language, studyProgramme.TitleEn, studyProgramme.TitleLv, studyProgramme.TitleRu) : ""
                };
        }

        public void Add(StudentModel studentModel)
        {
            if (studentModel != null)
            {
                Database.StudentService.Add(new Entities.Student
                {
                    Code = studentModel.Code,
                    Name = studentModel.Name,
                    Surname = studentModel.Surname,
                    StudyProgrammeId = studentModel.StudyProgrammeId,
                    Language = (SudentLanguage)studentModel.Language,
                    LearningForm = (LearningForm)studentModel.LearningForm,
                    Status = (StudentStatus)studentModel.Status,
                    Phone1 = studentModel.Phone1,
                    Phone2 = studentModel.Phone2,
                    Email1 = studentModel.Email1,
                    Email2 = studentModel.Email2,
                    Skype = studentModel.Skype,
                    Comment = studentModel.Comment
                });
            }
        }

        public void Update(StudentModel studentModel)
        {
            if (studentModel != null)
            {
                Database.StudentService.Update(new Entities.Student
                {
                    Id = studentModel.Id,
                    Code = studentModel.Code,
                    Name = studentModel.Name,
                    Surname = studentModel.Surname,
                    StudyProgrammeId = studentModel.StudyProgrammeId,
                    Language = (SudentLanguage)studentModel.Language,
                    LearningForm = (LearningForm)studentModel.LearningForm,
                    Status = (StudentStatus)studentModel.Status,
                    Phone1 = studentModel.Phone1,
                    Phone2 = studentModel.Phone2,
                    Email1 = studentModel.Email1,
                    Email2 = studentModel.Email2,
                    Skype = studentModel.Skype,
                    Comment = studentModel.Comment
                });
            }
        }
    }
}
