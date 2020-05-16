using SAMS.BusinessLogic.DatabaseInterfaces.Services;
using SAMS.BusinessLogic.Entities;
using SAMS.Database.EF.EntityFramework;
using System.Linq;

namespace SAMS.Database.EF.Services
{
    public class StudentService : IStudentService
    {
        private DataContext dataContext;
        public StudentService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Student> GetAll()
        {
            return dataContext.Students
                .Select(f => new Student
                {
                    Id = f.Id,
                    Code = f.Code,
                    Name = f.Name,
                    Surname = f.Surname,
                    StudyProgrammeId = f.StudyProgrammeId,
                    Language = f.Language,
                    LearningForm = f.LearningForm,
                    Status = f.Status,
                    Phone1 = f.Phone1,
                    Phone2 = f.Phone2,
                    Email1 = f.Email1,
                    Email2 = f.Email2,
                    Skype = f.Skype,
                    Comment = f.Comment,
                    StudentAttendances = f.StudentAttendances
                        .Select(s => new StudentAttendance {
                            Id = s.Id,
                            StudentId = s.StudentId,
                            SubjectId = s.SubjectId,
                            Date = s.Date,
                            RealAttendance = s.RealAttendance,
                            NecessaryAttendance = s.NecessaryAttendance
                        }).AsQueryable(),

                });
        }

        public Student Get(int id)
        {
            var student = dataContext.Students
                .FirstOrDefault(f => f.Id == id);
            return student?.MapToEntity();
        }

        public Student Add(Student student)
        {
            var studentToDb = new EntitiesDb.Student().MapFromEntity(student);
            var studentFromDb = dataContext.Add(studentToDb).Entity;
            dataContext.SaveChanges();
            return studentFromDb.MapToEntity();
        }


        public Student Update(Student studnet)
        {
            var studnetToDb = new EntitiesDb.Student().MapFromEntity(studnet);
            var studentFromDb = dataContext.Update(studnetToDb).Entity;

            dataContext.SaveChanges();
            return studentFromDb.MapToEntity();
        }
    }
}
