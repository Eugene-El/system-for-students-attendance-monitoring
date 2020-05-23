using SAMS.BusinessLogic.Entities;
using System.Linq;

namespace SAMS.BusinessLogic.DatabaseInterfaces.Services
{
    public interface IFacultyService
    {
        IQueryable<Faculty> GetAll();
        Faculty Get(int id);
        Faculty Add(Faculty faculty);
        Faculty Update(Faculty faculty);
    }
}
