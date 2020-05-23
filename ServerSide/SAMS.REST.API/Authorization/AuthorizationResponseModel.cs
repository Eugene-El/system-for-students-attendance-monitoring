namespace SAMS.REST.API.Authorization
{
    public class AuthorizationResponseModel
    {
        public bool IsAuthorized { get; set; }
        public bool IsKnownUser { get; set; }
        public Role Role { get; set; }
    }
}
