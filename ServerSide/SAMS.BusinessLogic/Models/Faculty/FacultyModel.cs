using SAMS.BusinessLogic.Models.StudyProgrammes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.Faculty
{
    public class FacultyModel
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string TitleLv { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }

        public string ShortTitleLv { get; set; }
        public string ShortTitleRu { get; set; }
        public string ShortTitleEn { get; set; }

        public List<StudyProgrammeModel> StudyProgrammes { get; set; }
    }
}
