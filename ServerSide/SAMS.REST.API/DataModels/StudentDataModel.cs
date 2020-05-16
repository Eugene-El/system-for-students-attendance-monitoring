using SAMS.BusinessLogic.Models.Common;
using SAMS.BusinessLogic.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.DataModels
{
    public class StudentDataModel
    {
        public StudentModel Student { get; set; }
        public IQueryable<ExtraSelectModel> Faculties { get; set; }
    }
}
