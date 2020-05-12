using SAMS.BusinessLogic.DatabaseInterfaces;
using SAMS.BusinessLogic.Factories;
using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
