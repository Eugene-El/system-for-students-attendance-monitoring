using System.Collections.Generic;
using System.Linq;

namespace SAMS.Database.EF.EntitiesDb
{
    public class Subject
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string TitleLv { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }

        public string ShortTitleLv { get; set; }
        public string ShortTitleRu { get; set; }
        public string ShortTitleEn { get; set; }

        public ICollection<StudentAttendance> StudentAttendances { get; set; }

        public BusinessLogic.Entities.Subject MapToEntity()
        {
            return new BusinessLogic.Entities.Subject
            {
                Id = this.Id,
                Code = this.Code,
                TitleLv = this.TitleLv,
                TitleRu = this.TitleRu,
                TitleEn = this.TitleEn,
                ShortTitleLv = this.ShortTitleLv,
                ShortTitleRu = this.ShortTitleRu,
                ShortTitleEn = this.ShortTitleEn,
            };
        }

        public Subject MapFromEntity(BusinessLogic.Entities.Subject subject)
        {
            Id = subject.Id;
            Code = subject.Code;
            TitleLv = subject.TitleLv;
            TitleRu = subject.TitleRu;
            TitleEn = subject.TitleEn;
            ShortTitleLv = subject.ShortTitleLv;
            ShortTitleRu = subject.ShortTitleRu;
            ShortTitleEn = subject.ShortTitleEn;
            return this;
        }
    }

}
