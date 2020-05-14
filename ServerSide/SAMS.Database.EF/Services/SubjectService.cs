using Microsoft.EntityFrameworkCore;
using SAMS.BusinessLogic.DatabaseInterfaces.Services;
using SAMS.BusinessLogic.Entities;
using SAMS.Database.EF.EntityFramework;
using System.Linq;

namespace SAMS.Database.EF.Services
{
    public class SubjectService : ISubjectService
    {
        private DataContext dataContext;
        public SubjectService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Subject> GetAll()
        {
            return dataContext.Subjects
                .Select(f => new Subject
                {
                    Id = f.Id,
                    Code = f.Code,
                    TitleEn = f.TitleEn,
                    TitleLv = f.TitleLv,
                    TitleRu = f.TitleRu,
                    ShortTitleEn = f.ShortTitleEn,
                    ShortTitleLv = f.ShortTitleLv,
                    ShortTitleRu = f.ShortTitleRu
                });
        }

        public Subject Get(int id)
        {
            var subject = dataContext.Subjects
                .FirstOrDefault(f => f.Id == id);
            return subject?.MapToEntity();
        }

        public Subject Add(Subject subject)
        {
            var subjectToDb = new EntitiesDb.Subject().MapFromEntity(subject);
            var subjectFromDb = dataContext.Add(subjectToDb).Entity;
            dataContext.SaveChanges();
            return subjectFromDb.MapToEntity();
        }


        public Subject Update(Subject subject)
        {
            var subjectToDb = new EntitiesDb.Subject().MapFromEntity(subject);
            var subjectFromDb = dataContext.Update(subjectToDb).Entity;

            dataContext.SaveChanges();
            return subjectFromDb.MapToEntity();
        }
    }
}
