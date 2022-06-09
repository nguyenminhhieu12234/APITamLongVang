using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Users;
using TuThienApi.Shared.Global;
using TuThienApi.Shared.ResponeModels;

namespace TuThienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        public AuthController(UserManager<UserEntity> userManager, IWebHostEnvironment WebHostEnviroment,
            ApplicationDbContext DbContext)
        {
            _userManager = userManager;
            _WebHostEnviroment = WebHostEnviroment;
            _DbContext = DbContext;
        }

        [HttpPost("reset-password")]
        public async Task<AppRespone<object>> ResetPassword(string useremail)
        {
            var mailConfig = _DbContext.SystemConfigs.FirstOrDefault();
            var bodyEmail = System.IO.File.ReadAllText(_WebHostEnviroment.WebRootPath + $"\\email\\BodyEmail\\reset_password.html");
            bodyEmail = bodyEmail.Replace("EmailUserRegister", useremail);
            bodyEmail = bodyEmail.Replace("paramvalue", useremail);
            if (Global.SendEmail(useremail, "Khôi phục mật khẩu người dùng!", bodyEmail, mailConfig.Email, mailConfig.EmailPassword, "smtp.gmail.com", 587, true))
                return AppRespone<object>.Success();
            return AppRespone<object>.Failed();

        }
    }
}
