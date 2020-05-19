using SAMS.BusinessLogic.DatabaseInterfaces.Services;
using SAMS.BusinessLogic.Entities;
using SAMS.Database.EF.EntityFramework;
using System.Linq;

namespace SAMS.Database.EF.Services
{
    public class StudentAttendanceService : IStudentAttendanceService
    {
        private DataContext dataContext;
        public StudentAttendanceService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<StudentAttendance> GetAllByStudentId(int id)
        {
            return dataContext.StudentAttendances
                .Where(s => s.StudentId == id)
                .Select(s => new StudentAttendance
                {
                    Id = s.Id,
                    StudentId = s.StudentId,
                    SubjectId = s.SubjectId,
                    Date = s.Date,
                    NecessaryAttendance = s.NecessaryAttendance,
                    RealAttendance = s.RealAttendance,
                });
        }

        public StudentAttendance Get(int id)
        {
            var studentAttendance = dataContext.StudentAttendances
                .FirstOrDefault(f => f.Id == id);
            return studentAttendance?.MapToEntity();
        }

        public StudentAttendance Add(StudentAttendance studentAttendance)
        {
            CheckDateAndSubjectUniqueness(studentAttendance);

            var studentAttendanceToDb = new EntitiesDb.StudentAttendance().MapFromEntity(studentAttendance);
            var studentAttendanceFromDb = dataContext.Add(studentAttendanceToDb).Entity;
            dataContext.SaveChanges();
            return studentAttendanceFromDb.MapToEntity();
        }


        public StudentAttendance Update(StudentAttendance studentAttendance)
        {
            CheckDateAndSubjectUniqueness(studentAttendance);

            var studentToDb = new EntitiesDb.StudentAttendance().MapFromEntity(studentAttendance);
            var studentFromDb = dataContext.Update(studentToDb).Entity;

            dataContext.SaveChanges();
            return studentFromDb.MapToEntity();
        }

        private void CheckDateAndSubjectUniqueness(StudentAttendance studentAttendance)
        {
            if (dataContext.StudentAttendances.Any(s => s.Id != studentAttendance.Id &&
                s.Date == studentAttendance.Date && s.SubjectId == studentAttendance.SubjectId))
                throw new System.Exception("Student attendance for this subject and day already exist!");
        }
    }
}
