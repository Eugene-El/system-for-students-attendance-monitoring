using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Models.Common;
using SAMS.BusinessLogic.Models.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMS.BusinessLogic.Factories
{
    public class FacultyFactory
    {
        protected IDatabase Database { get; private set; }
        public FacultyFactory(IDatabase database)
        {
            Database = database;
        }

        private string SelectLocalization(Language language, string eng, string lv, string ru)
        {
            return language == Language.English ? eng : (language == Language.Latvian ? lv : ru);
        }

        public IQueryable<FacultyGridModel> GetAllForGrid(Language language)
        {
            return Database.FacultyService.GetAll()
                .Select(f => new FacultyGridModel
                {
                    Id = f.Id,
                    Code = f.Code,
                    Title = SelectLocalization(language, f.TitleEn, f.TitleLv, f.TitleRu),
                    StudyProgrammeCount = f.StudyProgrammes.Count()
                });
        }
    }
}
