using Microsoft.AspNetCore.Mvc;
using SAMS.BusinessLogic;
using SAMS.BusinessLogic.Models.Common;
using SAMS.Database.EF;
using SAMS.Database.EF.EntityFramework;
using SAMS.REST.API.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAMS.REST.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/{id?}")]
    public class MainController : ControllerBase
    {
        protected FactoryConcentrator FactoryConcentrator;
        protected static List<AuthorizedUser> AuthorizedUsers = new List<AuthorizedUser>();

        public MainController(DataContext dataContext)
        {
            FactoryConcentrator = new FactoryConcentrator(new DatabaseEF(dataContext));
        }

        protected void CheckAuthorization(params Role[] roles)
        {
            string token = Request.Headers["token"];

            UpdateUsers();

            if (!AuthorizedUsers.Any(u => u.Token == token && roles.Contains(u.Role)))
                throw new Exception("401 - Unauthorized");
        }

        protected void UpdateUsers()
        {
            AuthorizedUsers = AuthorizedUsers.Where(u => u.TokenExpirationTime > DateTime.UtcNow).ToList();
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
