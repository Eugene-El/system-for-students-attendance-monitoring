﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.TTI.REST.API.Models
{
    public class Faculty
    {
        public int FacultyCode { get; set; }
        public string Title_LV { get; set; }
        public string Title_RU { get; set; }
        public string Title_EN { get; set; }
        public string Short_title_LV { get; set; }
        public string Short_title_RU { get; set; }
        public string Short_title_EN { get; set; }
        public List<StudyProgramme> Studyprogrammes { get; set; }
    }
}
