using Microsoft.AspNetCore.Mvc;
using SAMS.BusinessLogic;
using SAMS.Database.EF;
using SAMS.Database.EF.EntityFramework;

namespace SAMS.REST.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/{id?}")]
    public class MainController : ControllerBase
    {
        protected FactoryConcentrator FactoryConcentrator;
        public MainController(DataContext dataContext)
        {
            FactoryConcentrator = new FactoryConcentrator(new DatabaseEF(dataContext));
        }
    }
}
