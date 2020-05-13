using System.Collections.Generic;
using System.Linq;

namespace SAMS.Database.EF.EntitiesDb
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string TitleLv { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }

        public string ShortTitleLv { get; set; }
        public string ShortTitleRu { get; set; }
        public string ShortTitleEn { get; set; }

        public virtual ICollection<StudyProgramme> StudyProgrammes { get; set; }

        public BusinessLogic.Entities.Faculty MapToEntity()
        {
            return new BusinessLogic.Entities.Faculty
            {
                Id = this.Id,
                Code = this.Code,
                TitleLv = this.TitleLv,
                TitleRu = this.TitleRu,
                TitleEn = this.TitleEn,
                ShortTitleLv = this.ShortTitleLv,
                ShortTitleRu = this.ShortTitleRu,
                ShortTitleEn = this.ShortTitleEn,
                StudyProgrammes = this.StudyProgrammes.Select(s => s.MapToEntity()).AsQueryable()
            };
        }

        public Faculty MapFromEntity(BusinessLogic.Entities.Faculty faculty)
        {
            Id = faculty.Id;
            Code = faculty.Code;
            TitleLv = faculty.TitleLv;
            TitleRu = faculty.TitleRu;
            TitleEn = faculty.TitleEn;
            ShortTitleLv = faculty.ShortTitleLv;
            ShortTitleRu = faculty.ShortTitleRu;
            ShortTitleEn = faculty.ShortTitleEn;
            StudyProgrammes = faculty.StudyProgrammes.Select(s => new StudyProgramme().MapFromEntity(s)).ToList();
            return this;
        }
    }
}
