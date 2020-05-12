using SAMS.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMS.BusinessLogic.DatabaseInterfaces.Services
{
    public interface IFacultyService
    {
        IQueryable<Faculty> GetAll();
        Faculty Get(int id);
        Faculty Add(Faculty faculty);
        Faculty Update(int id, Faculty faculty);
    }
}
