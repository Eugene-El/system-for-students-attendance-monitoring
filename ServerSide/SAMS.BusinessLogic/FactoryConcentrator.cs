using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Factories;

namespace SAMS.BusinessLogic
{
    public class FactoryConcentrator
    {
        protected IDatabase Database { get; private set; }
        public FactoryConcentrator(IDatabase database)
        {
            Database = database;
        }

        private FacultyFactory facultyFactory;
        public FacultyFactory FacultyFactory
        {
            get
            {
                if (facultyFactory == null)
                    facultyFactory = new FacultyFactory(Database);
                return facultyFactory;
            }
        }


        private SubjectFactory subjectFactory;
        public SubjectFactory SubjectFactory
        {
            get
            {
                if (subjectFactory == null)
                    subjectFactory = new SubjectFactory(Database);
                return subjectFactory;
            }
        }


        private StudentFacotry studentFacotry;
        public StudentFacotry StudentFacotry
        {
            get
            {
                if (studentFacotry == null)
                    studentFacotry = new StudentFacotry(Database);
                return studentFacotry;
            }
        }


        private StudentAttendanceFactory studentAttendanceFactory;
        public StudentAttendanceFactory StudentAttendanceFactory
        {
            get
            {
                if (studentAttendanceFactory == null)
                    studentAttendanceFactory = new StudentAttendanceFactory(Database);
                return studentAttendanceFactory;
            }
        }


        private ConfigurationFactory configurationFactory;
        public ConfigurationFactory ConfigurationFactory
        {
            get
            {
                if (configurationFactory == null)
                    configurationFactory = new ConfigurationFactory(Database);
                return configurationFactory;
            }
        }

        private NotificationRuleFactory notificationRuleFactory;
        public NotificationRuleFactory NotificationRuleFactory
        {
            get
            {
                if (notificationRuleFactory == null)
                    notificationRuleFactory = new NotificationRuleFactory(Database);
                return notificationRuleFactory;
            }
        }

        private NotificationHistoryFactory notificationHistoryFactory;
        public NotificationHistoryFactory NotificationHistoryFactory
        {
            get
            {
                if (notificationHistoryFactory == null)
                    notificationHistoryFactory = new NotificationHistoryFactory(Database);
                return notificationHistoryFactory;
            }
        }
    }
}
