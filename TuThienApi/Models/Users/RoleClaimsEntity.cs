using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Models.Users
{
    public class RoleClaimsEntity:IdentityRoleClaim<int>
    {
        public bool IsDeleted { get; set; }
    }
}
