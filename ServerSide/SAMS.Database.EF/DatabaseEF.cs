using Microsoft.EntityFrameworkCore;
using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.DatabaseInterfaces.Services;
using SAMS.Database.EF.EntityFramework;
using SAMS.Database.EF.Services;
using System;

namespace SAMS.Database.EF
{
    public class DatabaseEF : IDatabase
    {
        private DataContext dataContext;
        public DatabaseEF(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        private FacultyService facultyService;
        public IFacultyService FacultyService
        {
            get
            {
                if (facultyService == null)
                    facultyService = new FacultyService(dataContext);
                return facultyService;
            }
        }

        private SubjectService subjectService;
        public ISubjectService SubjectService
        {
            get
            {
                if (subjectService == null)
                    subjectService = new SubjectService(dataContext);
                return subjectService;
            }
        }
    }
}
