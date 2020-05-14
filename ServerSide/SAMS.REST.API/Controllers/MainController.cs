using Microsoft.AspNetCore.Mvc;
using SAMS.BusinessLogic;
using SAMS.BusinessLogic.Models.Common;
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

        protected Language CurrentLanguage {
            get {
                Language currentLanguage;
                string language = Request.Headers["language"];
                switch (language)
                {
                    case "lv-LV":
                        currentLanguage = Language.Latvian;
                        break;

                    case "ru-RU":
                        currentLanguage = Language.Russian;
                        break;

                    case "en-GB":
                    default:
                        currentLanguage = Language.English;
                        break;
                }
                return currentLanguage;
            }
        }
    }
}
