using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Models.Common;
using SAMS.BusinessLogic.Models.Subjects;
using System.Linq;

namespace SAMS.BusinessLogic.Factories
{
    public class SubjectFactory : MainFactory
    {
        protected IDatabase Database { get; private set; }
        public SubjectFactory(IDatabase database)
        {
            Database = database;
        }


        public IQueryable<SubjectGridModel> GetAllForGrid(Language language)
        {
            return Database.SubjectService.GetAll()
                .Select(s => new SubjectGridModel
                {
                    Id = s.Id,
                    Code = s.Code,
                    Title = SelectLocalization(language, s.TitleEn, s.TitleLv, s.TitleRu)
                });
        }

        public IQueryable<SelectModel> GetAllSelectModel(Language language)
        {
            return Database.SubjectService.GetAll()
                .Select(s => new SelectModel
                {
                    Id = s.Id,
                    Title = SelectLocalization(language, s.TitleEn, s.TitleLv, s.TitleRu)
                });
        }

        public SubjectModel Get(int id)
        {
            var subject = Database.SubjectService.Get(id);
            return subject == null ?
                null : new SubjectModel
                {
                    Id = subject.Id,
                    Code = subject.Code,
                    TitleEn = subject.TitleEn,
                    TitleLv = subject.TitleLv,
                    TitleRu = subject.TitleRu,
                    ShortTitleEn = subject.ShortTitleEn,
                    ShortTitleLv = subject.ShortTitleLv,
                    ShortTitleRu = subject.ShortTitleRu
                };
        }

        public void Add(SubjectModel subjectModel)
        {
            if (subjectModel != null)
            {
                Database.SubjectService.Add(new Entities.Subject
                {
                    Code = subjectModel.Code,
                    TitleEn = subjectModel.TitleEn,
                    TitleLv = subjectModel.TitleLv,
                    TitleRu = subjectModel.TitleRu,
                    ShortTitleEn = subjectModel.ShortTitleEn,
                    ShortTitleLv = subjectModel.ShortTitleLv,
                    ShortTitleRu = subjectModel.ShortTitleRu
                });
            }
        }

        public void Update(SubjectModel subjectModel)
        {
            if (subjectModel != null)
            {
                Database.SubjectService.Update(new Entities.Subject
                {
                    Id = subjectModel.Id,
                    Code = subjectModel.Code,
                    TitleEn = subjectModel.TitleEn,
                    TitleLv = subjectModel.TitleLv,
                    TitleRu = subjectModel.TitleRu,
                    ShortTitleEn = subjectModel.ShortTitleEn,
                    ShortTitleLv = subjectModel.ShortTitleLv,
                    ShortTitleRu = subjectModel.ShortTitleRu
                });
            }
        }
    }
}
