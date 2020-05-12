using SAMS.BusinessLogic.DatabaseInterfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.BusinessLogic.DatabaseInterfaces
{
    public interface IDatabase
    {
        public IFacultyService FacultyService { get; }
    }
}
