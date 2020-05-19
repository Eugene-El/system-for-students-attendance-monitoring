using System.Collections.Generic;

namespace SAMS.Database.EF.EntitiesDb
{
    public class StudyProgramme
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string TitleLv { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public BusinessLogic.Entities.StudyProgramme MapToEntity()
        {
            return new BusinessLogic.Entities.StudyProgramme
            {
                Id = this.Id,
                Code = this.Code,
                TitleLv = this.TitleLv,
                TitleRu = this.TitleRu,
                TitleEn = this.TitleEn,
                StudentCount = Students != null ? Students.Count : 0
            };
        }

        public StudyProgramme MapFromEntity(BusinessLogic.Entities.StudyProgramme studyProgramme)
        {
            Id = studyProgramme.Id;
            Code = studyProgramme.Code;
            TitleLv = studyProgramme.TitleLv;
            TitleRu = studyProgramme.TitleRu;
            TitleEn = studyProgramme.TitleEn;
            return this;
        }
    }
}
