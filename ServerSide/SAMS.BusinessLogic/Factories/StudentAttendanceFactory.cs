using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Entities.Enumerations;
using SAMS.BusinessLogic.Models.Common;
using SAMS.BusinessLogic.Models.StudentAttendances;
using SAMS.BusinessLogic.Models.StudentAttendances.Statistics;
using System;
using System.Collections.Generic;
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

        public AttendanceStatisticsModel GetStatistics(Language language, int studentId, DateTime from, DateTime to)
        {
            var subjects = Database.SubjectService.GetAll().Select(sub => new SelectModel
            {
                Id = sub.Id,
                Title = SelectLocalization(language, sub.TitleEn, sub.TitleLv, sub.TitleRu)
            });
            var studentAttendances = Database.StudentAttendanceService.GetAllByStudentId(studentId)
                .Where(a => a.Date >= from && a.Date <= to)
                .ToList();

            DateTime counter = from;
            AttendanceStatisticsModel result = new AttendanceStatisticsModel();

            while (counter <= to)
            {
                var dayAttendances = studentAttendances.Where(a => a.Date == counter).ToList();

                if (dayAttendances.Any())
                {
                    result.GlobalStatistics.Add(new DayStatisticsModel
                    {
                        Date = counter.ToString("dd.MM.yyyy"),
                        AttendanceProcent = (int)(dayAttendances.Sum(s => 100d * s.RealAttendance / s.NecessaryAttendance) / dayAttendances.Count())
                    });

                    dayAttendances.ForEach(day =>
                    {
                        var subjectStat = result.SubjectStatistics.FirstOrDefault(s => s.SubjectId == day.SubjectId);

                        if (subjectStat == null)
                        {
                            subjectStat = new SubjectStatisticsModel
                            {
                                SubjectId = day.SubjectId,
                                Title = subjects.FirstOrDefault(s => s.Id == day.SubjectId)?.Title ?? ""
                            };
                            result.SubjectStatistics.Add(subjectStat);
                        }

                        subjectStat.DayStatistics.Add(new DayStatisticsModel
                        {
                            Date = counter.ToString("dd.MM.yyyy"),
                            AttendanceProcent = (int)(100d * day.RealAttendance / day.NecessaryAttendance)
                        });
                    });
                }

                counter = counter.AddDays(1);
            }
            result.GlobalAttendanceProcent = (int)(studentAttendances.Sum(s => 100d * s.RealAttendance / s.NecessaryAttendance) / studentAttendances.Count());

            return result;
        }

        public List<FacultyAttendanceModel> GetStatisticsByFaculties(Language language, DateTime from, DateTime to)
        {
            var faculties = Database.FacultyService.GetAll()
                .Select(f => new ExtraSelectModel
                {
                    Id = f.Id,
                    Title = SelectLocalization(language, f.TitleEn, f.TitleLv, f.TitleRu),
                    Options = f.StudyProgrammes.Select(s => new SelectModel
                    {
                        Id = s.Id,
                        Title = SelectLocalization(language, s.TitleEn, s.TitleLv, s.TitleRu)
                    })
                })
                .ToList();
           var students = Database.StudentService.GetAll()
                .Select(s => new
                {
                    Id = s.Id,
                    StudyProgrammeId = s.StudyProgrammeId
                })
                .ToList()
                .Select(s => new
                {
                    Id = s.Id,
                    FacltyId = faculties.First(f => f.Options.Any(sp => sp.Id == s.StudyProgrammeId)).Id
                }).ToList();
            var studentAttendances = Database.StudentAttendanceService.GetAll()
                .Select(a => new
                {
                    Id = a.Id,
                    Date = a.Date,
                    RealAttendance = a.RealAttendance,
                    NecessaryAttendance = a.NecessaryAttendance,
                    StudentId = a.StudentId
                })
                .ToList()
                .Select(a => new {
                    Id = a.Id,
                    Date = a.Date,
                    RealAttendance = a.RealAttendance,
                    NecessaryAttendance = a.NecessaryAttendance,
                    FacultyId = students.First(s => s.Id == a.StudentId).FacltyId
                })
                .Where(a => a.Date >= from && a.Date <= to)
                .ToList();

            List<FacultyAttendanceModel> facultyAttendances = new List<FacultyAttendanceModel>();

            faculties.ForEach(faculty =>
            {
                var attendances = studentAttendances.Where(a => a.FacultyId == faculty.Id);
                double percent = 100d * attendances.Sum(a => a.RealAttendance) / attendances.Sum(a => a.NecessaryAttendance);
                percent = ((int)(percent * 100)) / 100d;
                facultyAttendances.Add(new FacultyAttendanceModel
                {
                    Title = faculty.Title,
                    AttendancePercent = percent
                });
            });

            return facultyAttendances;
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
