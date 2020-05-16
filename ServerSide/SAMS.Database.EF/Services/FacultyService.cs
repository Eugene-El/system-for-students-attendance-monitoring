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
                            TitleRu = s.TitleRu
                        }).AsQueryable()
                });
        }

        public Faculty Get(int id)
        {
            var faculty = dataContext.Faculties
                .Include(f => f.StudyProgrammes)
                .FirstOrDefault(f => f.Id == id);
            return faculty == null ? null : faculty.MapToEntity();
        }

        public Faculty Add(Faculty faculty)
        {
            var facultyToDb = new EntitiesDb.Faculty().MapFromEntity(faculty);
            var facultyFromDb = dataContext.Add(facultyToDb).Entity;
            dataContext.SaveChanges();
            return facultyFromDb.MapToEntity();
        }


        public Faculty Update(Faculty faculty)
        {
            var facultyToDb = new EntitiesDb.Faculty().MapFromEntity(faculty);
            var facultyFromDb = dataContext.Update(facultyToDb).Entity;

            var studyProgrammesFromDb = dataContext.StudyProgrammes.Where(s => s.FacultyId == facultyFromDb.Id).ToList();
            var programmesToDelete = studyProgrammesFromDb.Where(s => !faculty.StudyProgrammes.Any(ss => ss.Id == s.Id)).ToList();
            dataContext.StudyProgrammes.RemoveRange(programmesToDelete);

            dataContext.SaveChanges();
            return facultyFromDb.MapToEntity();
        }
    }
}
