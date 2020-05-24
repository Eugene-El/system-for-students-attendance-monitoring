using SAMS.BusinessLogic.DatabaseInterfaces.Services;

namespace SAMS.BusinessLogic.DatabaseInterfaces
{
    public interface IDatabase
    {
        public IFacultyService FacultyService { get; }
        public ISubjectService SubjectService { get; }
        public IStudentService StudentService { get; }
        public IStudentAttendanceService StudentAttendanceService { get; }
        public IConfigurationService ConfigurationService { get; }
        public INotificationRuleService NotificationRuleService { get; }
    }
}
