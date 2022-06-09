using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.General;
using TuThienApi.Shared.Extensions;

namespace TuThienApi.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public CustomMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, ApplicationDbContext DbContext)
        {
            await ValidateToken(context);
            await _next(context);
        }

        private async Task ValidateToken(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            token = token.Replace("Bearer ", "");
            if(token != "")
            {
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authSigningKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jsonToken = (JwtSecurityToken)validatedToken;

                context.Items[ClaimsTypes.UserId] = jsonToken.Claims.Where(x => x.Type == ClaimsTypes.UserId).FirstOrDefault().Value;
                context.Items[ClaimsTypes.Avatar] = jsonToken.Claims.Where(x => x.Type == ClaimsTypes.Avatar).FirstOrDefault().Value;
                context.Items[ClaimsTypes.UserName] = jsonToken.Claims.Where(x => x.Type == ClaimsTypes.UserName).FirstOrDefault().Value;
            }
        }

        //private async Task RecordLogsRequest(HttpContext context, ApplicationDbContext DbContext)
        //{
        //    await Task.CompletedTask;
        //    //int userRequest = Convert.ToInt32(context.User.FindFirst(ClaimsTypes.UserId).Value);
        //    var newLogs = new SystemLogsEntity {
        //        RequestBody = context.Request.Headers["Referer"],
        //        CreateTime = DateTime.Now,
        //        CreateUser = 1,
        //        RequestType = context.Request.Method
        //    };
        //    DbContext.Logs.Add(newLogs);
        //    await DbContext.SaveChangesAsync();
        //}

        //private async Task RecordLogsRespone(HttpContext context, ApplicationDbContext DbContext)
        //{

        //}
    }
}
