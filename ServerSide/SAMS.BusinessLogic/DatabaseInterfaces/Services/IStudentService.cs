using SAMS.BusinessLogic.Entities;
using System.Linq;

namespace SAMS.BusinessLogic.DatabaseInterfaces.Services
{
    public interface IStudentService
    {
        IQueryable<Student> GetAll();
        Student Get(int id);
        Student Add(Student student);
        Student Update(Student student);
    }
}
