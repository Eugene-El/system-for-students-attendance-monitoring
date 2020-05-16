using System.Collections.Generic;
using System.Linq;

namespace SAMS.BusinessLogic.Models.Common
{
    public class ExtraSelectModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<SelectModel> Options { get; set; }
    }
}
