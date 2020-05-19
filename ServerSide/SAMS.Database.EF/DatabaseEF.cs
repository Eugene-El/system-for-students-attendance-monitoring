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


        private StudentService studentService;
        public IStudentService StudentService
        {
            get
            {
                if (studentService == null)
                    studentService = new StudentService(dataContext);
                return studentService;
            }
        }

        private StudentAttendanceService studentAttendanceService;
        public IStudentAttendanceService StudentAttendanceService
        {
            get
            {
                if (studentAttendanceService == null)
                    studentAttendanceService = new StudentAttendanceService(dataContext);
                return studentAttendanceService;
            }
        }


        private ConfigurationService configurationService;
        public IConfigurationService ConfigurationService
        {
            get
            {
                if (configurationService == null)
                    configurationService = new ConfigurationService(dataContext);
                return configurationService;
            }
        }
    }
}
