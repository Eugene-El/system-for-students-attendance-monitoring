using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Entities.Enumerations;
using SAMS.BusinessLogic.Models.Common;
using SAMS.BusinessLogic.Models.StudentAttendances;
using System.Linq;

namespace SAMS.BusinessLogic.Factories
{
    public class StudentAttendanceFactory : MainFactory
    {
        protected IDatabase Database { get; private set; }
        public StudentAttendanceFactory(IDatabase database)
        {
            Database = database;
        }

        public IQueryable<StudentAttandanceGridModel> GetAllForGrid(Language language, int studentId)
        {
            var subjects = Database.SubjectService.GetAll().Select(sub => new SelectModel
            {
                Id = sub.Id,
                Title = SelectLocalization(language, sub.TitleEn, sub.TitleLv, sub.TitleRu)
            });
            return Database.StudentAttendanceService.GetAllByStudentId(studentId)
                .Select(s => new {
                    Id = s.Id,
                    SubjectTitle = subjects.FirstOrDefault(sub => sub.Id == s.SubjectId).Title,
                    Date = s.Date,
                    NecessaryAttendance = s.NecessaryAttendance,
                    RealAttendance = s.RealAttendance
                }).OrderByDescending(s => s.Date)
                .AsEnumerable()
                .Select(s => new StudentAttandanceGridModel {
                    Id = s.Id,
                    SubjectTitle = s.SubjectTitle,
                    Date = s.Date.ToString("dd.MM.yyyy"),
                    NecessaryAttendance = s.NecessaryAttendance,
                    RealAttendance = s.RealAttendance
            }).AsQueryable();
        }

        public StudentAttendanceModel Get(int id)
        {
            var studentAttendance = Database.StudentAttendanceService.Get(id);
            return studentAttendance == null ?
                null : new StudentAttendanceModel
                {
                    Id = studentAttendance.Id,
                    StudentId = studentAttendance.StudentId,
                    SubjectId = studentAttendance.SubjectId,
                    Date = studentAttendance.Date,
                    RealAttendance = studentAttendance.RealAttendance,
                    NecessaryAttendance = studentAttendance.NecessaryAttendance,
                };
        }

        public void Add(StudentAttendanceModel studentAttendanceModel)
        {
            if (studentAttendanceModel != null)
            {
                Database.StudentAttendanceService.Add(new Entities.StudentAttendance
                {
                    Id = studentAttendanceModel.Id,
                    StudentId = studentAttendanceModel.StudentId,
                    SubjectId = studentAttendanceModel.SubjectId,
                    Date = studentAttendanceModel.Date,
                    RealAttendance = studentAttendanceModel.RealAttendance,
                    NecessaryAttendance = studentAttendanceModel.NecessaryAttendance
                });
            }
        }

        public void Update(StudentAttendanceModel studentAttendanceModel)
        {
            if (studentAttendanceModel != null)
            {
                Database.StudentAttendanceService.Update(new Entities.StudentAttendance
                {
                    Id = studentAttendanceModel.Id,
                    StudentId = studentAttendanceModel.StudentId,
                    SubjectId = studentAttendanceModel.SubjectId,
                    Date = studentAttendanceModel.Date,
                    RealAttendance = studentAttendanceModel.RealAttendance,
                    NecessaryAttendance = studentAttendanceModel.NecessaryAttendance
                });
            }
        }

    }
}
