using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMS.REST.API.Authorization
{
    public class AuthorizedUser
    {
        public string Login { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpirationTime { get; set; }
        public Role Role { get; set; }
    }
}
