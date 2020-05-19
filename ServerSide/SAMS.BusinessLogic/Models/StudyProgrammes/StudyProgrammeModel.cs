using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.Models.StudyProgrammes
{
    public class StudyProgrammeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string TitleLv { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }

        public int StudentsCount { get; set; }
    }
}
