using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Users;
using TuThienApi.Shared.Extensions;

namespace TuThienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController() { }

        protected int CurrentUser 
        {
            get
            {
                if (HttpContext.Items[ClaimsTypes.UserId] != null)
                {
                    return Convert.ToInt32(HttpContext.Items[ClaimsTypes.UserId].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        public UserManager<UserEntity> _userManager { get; set; }
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public IConfiguration _configuration { get; set; }
        public SignInManager<UserEntity> _signInManager { get; set; }
        public ApplicationDbContext _DbContext { get; set; }
        public IWebHostEnvironment _WebHostEnviroment { get; set; }
    }
}
