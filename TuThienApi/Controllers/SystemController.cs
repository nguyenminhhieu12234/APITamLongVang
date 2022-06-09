using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.General;
using TuThienApi.Shared.RequestModels.System;
using TuThienApi.Shared.ResponeModels;

namespace TuThienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : BaseApiController
    {

        public SystemController(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpPost("create-system")]
        public async Task<AppRespone<object>> CreateSystem(SystemRequest data)
        {
            try
            {
                var checkSystem = _DbContext.SystemConfigs.Count();
                if(checkSystem == 0)
                { 
                    var newsystem = new SystemConfigEntity
                    {
                        Name = data.Name,
                        Email = data.Email,
                        EmailPassword = data.EmailPassword,
                        PhoneNumber = data.PhoneNumber,
                        Address = data.Address,
                        Facebook = data.Facebook,
                        Youtube = data.Youtube,
                        Instagram = data.Instagram,
                        Medium = data.Medium,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser
                    };
                    _DbContext.SystemConfigs.Add(newsystem);
                    await _DbContext.SaveChangesAsync();
                    return AppRespone<object>.Success("Thêm cấu hình hệ thống thành công!");
                }
                else
                {
                    return AppRespone<object>.Failed("Cấu hình hệ thống đã tồn tại!");
                }
            }
            catch
            {
                return AppRespone<object>.Failed("Thêm cấu hình hệ thống thất bại!");
            }
        }

        [HttpPost("create-email-content")]
        public async Task<AppRespone<object>> CreateEmailContent(int systemid, EmailContentRequest data)
        {
            try
            {
                var newContent = new EmailContentEntity
                {
                    Title = data.Title,
                    Content = data.Content,
                    SystemConfigId = systemid
                };
                _DbContext.EmailContents.Add(newContent);
                await _DbContext.SaveChangesAsync();

                return AppRespone<object>.Success("Thêm nội dung Email thành công!");

            }
            catch
            {
                return AppRespone<object>.Failed("Thêm nội dung Email không thành công!");
            }
        }

        [HttpPut("update-system/{id}")]
        public async Task<AppRespone<object>> updatesystem(int id, SystemRequest data)
        {
            try
            {
                var currentSystem = _DbContext.SystemConfigs.Where(x => x.Id == id).SingleOrDefault();
                currentSystem.Name = data.Name;
                currentSystem.Email = data.Email;
                currentSystem.EmailPassword = data.EmailPassword;
                currentSystem.UpdateTime = DateTime.Now;
                currentSystem.UpdateUser = CurrentUser;
                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success("Cập nhật thông tin hệ thống thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Cập nhật tài khoản không thành công!");
            }
        }
    }
}
