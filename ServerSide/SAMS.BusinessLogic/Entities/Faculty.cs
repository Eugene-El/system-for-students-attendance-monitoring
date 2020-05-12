using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMS.BusinessLogic.Entities
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

        public IQueryable<StudyProgramme> StudyProgrammes { get; set; }
    }
}
