﻿using SAMS.BusinessLogic.Entities;
using System.Linq;

namespace SAMS.BusinessLogic.DatabaseInterfaces.Services
{
    public interface ISubjectService
    {
        IQueryable<Subject> GetAll();
        Subject Get(int id);
        Subject Add(Subject subject);
        Subject Update(Subject subject);
    }
}
