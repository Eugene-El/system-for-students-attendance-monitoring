using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emulator.TTI.AuthorizationService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Emulator.TTI.AuthorizationService.Controllers
{
    [ApiController]
    [Route("api/[action]/")]
    public class AuthorizationController : ControllerBase
    {
        protected List<UserDataModel> GetUsers()
        {
            string pathToFile = "Data/users.json";
            if (System.IO.File.Exists(pathToFile))
            {
                var myJsonString = System.IO.File.ReadAllText(pathToFile);
                List<UserDataModel> data = JsonConvert.DeserializeObject<List<UserDataModel>>(myJsonString);
                return data;
            }
            return new List<UserDataModel>();
        }

        [HttpPost]
        public IActionResult Authorize(AuthorizationModel authorizationModel)
        {
            try
            {
                var response = new AuthorizationResponseModel();

                var user = GetUsers().FirstOrDefault(u => u.Login == authorizationModel.Login);

                if (user == null)
                    return Ok(response);

                response.IsKnownUser = true;

                if (user.Password != authorizationModel.Password)
                    return Ok(response);

                response.IsAuthorized = true;
                response.Role = user.Role;

                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
