using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.Database.EF.EntitiesDb
{
    public class Configuration
    {
        public int Id { get; set; }
        public ConfigurationType Type { get; set; }
        public string Content { get; set; }
    }
}
