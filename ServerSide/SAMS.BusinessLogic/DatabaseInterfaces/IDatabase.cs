using SAMS.BusinessLogic.DatabaseInterfaces.Services;

namespace SAMS.BusinessLogic.DatabaseInterfaces
{
    public interface IDatabase
    {
        public IFacultyService FacultyService { get; }
        public ISubjectService SubjectService { get; }
    }
}
