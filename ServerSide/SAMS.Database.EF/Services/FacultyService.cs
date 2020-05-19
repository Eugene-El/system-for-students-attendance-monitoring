using Microsoft.EntityFrameworkCore;
using SAMS.BusinessLogic.DatabaseInterfaces.Services;
using SAMS.BusinessLogic.Entities;
using SAMS.Database.EF.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace SAMS.Database.EF.Services
{
    public class FacultyService : IFacultyService
    {
        private DataContext dataContext;
        public FacultyService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Faculty> GetAll()
        {
            return dataContext.Faculties
                .Include(f => f.StudyProgrammes)
                .ThenInclude(s => s.Students)
                .Select(f => new Faculty
                {
                    Id = f.Id,
                    Code = f.Code,
                    TitleEn = f.TitleEn,
                    TitleLv = f.TitleLv,
                    TitleRu = f.TitleRu,
                    ShortTitleEn = f.ShortTitleEn,
                    ShortTitleLv = f.ShortTitleLv,
                    ShortTitleRu = f.ShortTitleRu,
                    StudyProgrammes = f.StudyProgrammes
                        .Select(s => new StudyProgramme
                        {
                            Id = s.Id,
                            Code = s.Code,
                            TitleEn = s.TitleEn,
                            TitleLv = s.TitleLv,
                            TitleRu = s.TitleRu,
                            StudentCount = s.Students != null ? s.Students.Count() : 0
                        }).AsQueryable()
                });
        }

        public Faculty Get(int id)
        {
            var faculty = dataContext.Faculties
                .Include(f => f.StudyProgrammes)
                .ThenInclude(s => s.Students)
                .FirstOrDefault(f => f.Id == id);
            return faculty == null ? null : faculty.MapToEntity();
        }

        public Faculty Add(Faculty faculty)
        {
            CheckCodeUniqueness(faculty);

            var facultyToDb = new EntitiesDb.Faculty().MapFromEntity(faculty);
            var facultyFromDb = dataContext.Add(facultyToDb).Entity;
            dataContext.SaveChanges();
            return facultyFromDb.MapToEntity();
        }


        public Faculty Update(Faculty faculty)
        {
            CheckCodeUniqueness(faculty);

            var facultyToDb = new EntitiesDb.Faculty().MapFromEntity(faculty);
            var facultyFromDb = dataContext.Update(facultyToDb).Entity;

            var studyProgrammesFromDb = dataContext.StudyProgrammes.Where(s => s.FacultyId == facultyFromDb.Id).ToList();
            var programmesToDelete = studyProgrammesFromDb.Where(s => !faculty.StudyProgrammes.Any(ss => ss.Id == s.Id)).ToList();
            dataContext.StudyProgrammes.RemoveRange(programmesToDelete);

            dataContext.SaveChanges();
            return facultyFromDb.MapToEntity();
        }

        private void CheckCodeUniqueness(Faculty faculty)
        {
            if (dataContext.Faculties.Any(f => f.Id != faculty.Id && f.Code == faculty.Code))
                throw new System.Exception("This faculty code already exist is system!");

            faculty.StudyProgrammes.ToList().ForEach(s => CheckCodeUniqueness(s));
        }

        private void CheckCodeUniqueness(StudyProgramme studyProgramme)
        {
            if (dataContext.StudyProgrammes.Any(f => f.Id != studyProgramme.Id && f.Code == studyProgramme.Code))
                throw new System.Exception("This study programme code already exist is system!");
        }
    }
}
