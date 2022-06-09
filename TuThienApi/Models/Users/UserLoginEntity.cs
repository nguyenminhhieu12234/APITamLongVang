using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Models.Users
{
    public class UserLoginEntity : IdentityUserLogin<int>
    {
        public bool IsDeleted { get; set; }
    }
}
