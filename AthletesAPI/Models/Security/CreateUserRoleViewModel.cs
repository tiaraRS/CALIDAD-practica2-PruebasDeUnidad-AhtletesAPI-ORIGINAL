using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Models.Security
{
    public class CreateUserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
