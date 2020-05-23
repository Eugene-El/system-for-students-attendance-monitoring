using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using SAMS.Database.EF.EntityFramework;
using SAMS.REST.API.Authorization;
using SAMS.REST.API.DataModels;
using System;
using System.Linq;

namespace SAMS.REST.API.Controllers
{
    public class AuthorizationController : MainController
    {
        protected AuthorizationServicePaths authorizationServicePaths;
        protected int tokenLifetimeMinutes = 30;
        public AuthorizationController(DataContext dataContext, IOptions<AuthorizationServicePaths> authorizationServicePaths) : base(dataContext)
        {
            this.authorizationServicePaths = authorizationServicePaths.Value;
        }


        [HttpPost]
        public IActionResult Authorize(AuthorizationModel authorizationModel)
        {
            try
            {
                var client = new RestClient(authorizationServicePaths.ServiceUrl);
                var request = new RestRequest(authorizationServicePaths.Method, DataFormat.Json);

                request.AddJsonBody(authorizationModel);
                var response = client.Post(request);

                if (!response.IsSuccessful)
                    throw new Exception("Authorization service error. Please try letter");
                    
                var authorizationResponse = JsonConvert.DeserializeObject<AuthorizationResponseModel>(response.Content);

                if (authorizationResponse == null)
                    throw new Exception("Authorization service messeging error. Please try letter");

                if (authorizationResponse.IsAuthorized)
                {
                    UpdateUsers();

                    var loginedUser = AuthorizedUsers.FirstOrDefault(u => u.Login == authorizationModel.Login);
                    if (loginedUser != null)
                        return Ok(loginedUser);


                    var user = new AuthorizedUser
                    {
                        Login = authorizationModel.Login,
                        Token = Guid.NewGuid().ToString(),
                        Role = authorizationResponse.Role,
                        TokenExpirationTime = DateTime.UtcNow.AddMinutes(tokenLifetimeMinutes)
                    };
                    AuthorizedUsers.Add(user);
                    return Ok(user);
                }

                throw new Exception(authorizationResponse.IsKnownUser ?
                    "Incorrect password. Please try again" : "Unknown user. Please change login and try again");
                
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost]
        public IActionResult CheckToken(TokenModel tokenModel)
        {
            try
            {
                UpdateUsers();

                return Ok(AuthorizedUsers.Any(u => u.Token == tokenModel.Token));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

    }
}
