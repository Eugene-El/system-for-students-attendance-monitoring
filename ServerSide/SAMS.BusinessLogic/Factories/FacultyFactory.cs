﻿using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Models.Common;
using SAMS.BusinessLogic.Models.Faculty;
using SAMS.BusinessLogic.Models.StudyProgrammes;
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

        private static string SelectLocalization(Language language, string eng, string lv, string ru)
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

        public FacultyModel Get(int id)
        {
            var faculty = Database.FacultyService.Get(id);
            return faculty == null ?
                null : new FacultyModel
                {
                    Id = faculty.Id,
                    Code = faculty.Code,
                    TitleEn = faculty.TitleEn,
                    TitleLv = faculty.TitleLv,
                    TitleRu = faculty.TitleRu,
                    ShortTitleEn = faculty.ShortTitleEn,
                    ShortTitleLv = faculty.ShortTitleLv,
                    ShortTitleRu = faculty.ShortTitleRu,
                    StudyProgrammes = faculty.StudyProgrammes
                        .Select(s => new StudyProgrammeModel
                        {
                            Id = s.Id,
                            Code = s.Code,
                            TitleEn = s.TitleEn,
                            TitleLv = s.TitleLv,
                            TitleRu = s.TitleRu
                        }).ToList()
                };
        }

        public void Add(FacultyModel facultyModel)
        {
            if (facultyModel != null)
            {
                Database.FacultyService.Add(new Entities.Faculty { 
                    Code = facultyModel.Code,
                    TitleEn = facultyModel.TitleEn,
                    TitleLv = facultyModel.TitleLv,
                    TitleRu = facultyModel.TitleRu,
                    ShortTitleEn = facultyModel.ShortTitleEn,
                    ShortTitleLv = facultyModel.ShortTitleLv,
                    ShortTitleRu = facultyModel.ShortTitleRu,
                    StudyProgrammes = facultyModel.StudyProgrammes
                        .Select(s => new Entities.StudyProgramme
                        {
                            Id = s.Id,
                            Code = s.Code,
                            TitleEn = s.TitleEn,
                            TitleLv = s.TitleLv,
                            TitleRu = s.TitleRu
                        }).AsQueryable()
                });
            }
        }

        public void Update(FacultyModel facultyModel)
        {
            if (facultyModel != null)
            {
                Database.FacultyService.Update(facultyModel.Id, new Entities.Faculty
                {
                    Id = facultyModel.Id,
                    Code = facultyModel.Code,
                    TitleEn = facultyModel.TitleEn,
                    TitleLv = facultyModel.TitleLv,
                    TitleRu = facultyModel.TitleRu,
                    ShortTitleEn = facultyModel.ShortTitleEn,
                    ShortTitleLv = facultyModel.ShortTitleLv,
                    ShortTitleRu = facultyModel.ShortTitleRu,
                    StudyProgrammes = facultyModel.StudyProgrammes
                        .Select(s => new Entities.StudyProgramme
                        {
                            Id = s.Id,
                            Code = s.Code,
                            TitleEn = s.TitleEn,
                            TitleLv = s.TitleLv,
                            TitleRu = s.TitleRu
                        }).AsQueryable()
                });
            }
        }
    }
}