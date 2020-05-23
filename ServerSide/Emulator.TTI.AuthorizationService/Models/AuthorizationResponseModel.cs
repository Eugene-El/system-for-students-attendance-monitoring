using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emulator.TTI.AuthorizationService.Models
{
    public class AuthorizationResponseModel
    {
        public bool IsAuthorized { get; set; }
        public bool IsKnownUser { get; set; }
        public Role Role { get; set; }
    }
}
