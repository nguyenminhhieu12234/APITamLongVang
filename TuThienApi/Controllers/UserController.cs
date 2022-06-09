using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Users;
using TuThienApi.Shared.Extensions;
using TuThienApi.Shared.Global;
using TuThienApi.Shared.RequestModels.User;
using TuThienApi.Shared.ResponeModels;
using TuThienApi.Shared.ResponeModels.TransactionHistory;
using TuThienApi.Shared.ResponeModels.User;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        public UserController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager
            , ApplicationDbContext DbContext, IConfiguration configuration, IWebHostEnvironment WebHostEnviroment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _DbContext = DbContext;
            _configuration = configuration;
            _WebHostEnviroment = WebHostEnviroment;
        }

        #region User
        [HttpPost("register-user")]
        public async Task<AppRespone<object>> RegisterUser(RegisterRequest data)
        {
            var checkUser = await _userManager.FindByEmailAsync(data.Email);
            if (checkUser != null)
                return AppRespone<object>.Failed("Tài khoản đã tồn tại!");

            UserEntity newUser = new UserEntity()
            {
                UserName = data.Email,
                FullName = data.FullName,
                Avatar = data.AvatarPath,
                PhoneNumber = data.PhoneNumber,
                Email = data.Email,
                Address = data.Address,
                CreateDate = DateTime.Now,
                Type = data.Type,
                Status = UserStatus.UnLock
            };
            var result = await _userManager.CreateAsync(newUser, data.Password);
            if(result.Succeeded)
            {
                //Xác minh email khi tạo tài khoản
                var mailConfig = _DbContext.SystemConfigs.FirstOrDefault();
                string codeConfirmEmail = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string BodyEmail = System.IO.File.ReadAllText(_WebHostEnviroment.WebRootPath + $"\\email\\BodyEmail\\email.html");
                BodyEmail = BodyEmail.Replace("EmailUserRegister", newUser.Email);
                BodyEmail = BodyEmail.Replace("paramvalue1", newUser.Email);
                BodyEmail = BodyEmail.Replace("paramvalue2", codeConfirmEmail);
                if (
                    Global.SendEmail(newUser.Email, "Xác minh tài khoản người dùng",
                    BodyEmail,
                    mailConfig.Email,
                    mailConfig.EmailPassword,
                    "smtp.gmail.com",
                    587,
                    true))
                    return AppRespone<object>.Success("Tạo tài khoản thành công vui lòng truy cập email đăng ký để tiến hành xác minh tài khoản!");
                else
                {
                    await _userManager.DeleteAsync(newUser);
                    return AppRespone<object>.Failed("Tạo tài khoản không thành công!");
                }
            }
            else
            {
                await _userManager.DeleteAsync(newUser);
                return AppRespone<object>.Failed("Tạo tài khoản không thành công!");
            }
        }

        [HttpPatch("confirm-email")]
        public async Task<object> ConfirmEmail(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return AppRespone<object>.Failed("Email đăng ký không tồn tại!");
            var confirm = await _userManager.ConfirmEmailAsync(user, code);
            if (confirm.Succeeded)
                return AppRespone<object>.Success("Xác minh tài khoản thành công!");
            return AppRespone<object>.Failed("Xác minh tài khoản không thành công!");
        }

        [HttpPost("admin-register")]
        public async Task<AppRespone<object>> AdminRegister(RegisterRequest data)
        {
            var checkUser = await _userManager.FindByEmailAsync(data.Email);
            if (checkUser != null)
                return AppRespone<object>.Failed("Tài khoản đã tồn tại!");

            UserEntity newUser = new UserEntity()
            {
                UserName = data.Email,
                FullName = data.FullName,
                Avatar = data.AvatarPath,
                PhoneNumber = data.PhoneNumber,
                Address = data.Address,
                Email = data.Email,
                IsAdmin = true,
                CreateDate = DateTime.Now,
                Type = data.Type
            };
            var result = await _userManager.CreateAsync(newUser, data.Password);
            if (result.Succeeded)
            {
                //Xác minh email khi tạo tài khoản
                return AppRespone<object>.Success("Tạo tài khoản thành công!");
            }
            else
            {
                await _userManager.DeleteAsync(newUser);
                return AppRespone<object>.Failed("Tạo tài khoản không thành công!");
            }
        }

        [HttpPost("login-user")]
        public async Task<AppRespone<object>> LoginUser(RequestLogin user)
        {
            var checkUser = await _userManager.FindByNameAsync(user.Email);
            if (checkUser == null)
                return AppRespone<object>.Failed("Tên đăng nhập hoặc mật khẩu không chính xác!");
            if(checkUser.IsAdmin != false)
                return AppRespone<object>.Failed("Tên đăng nhập hoặc mật khẩu không chính xác!");
            var result = await _signInManager.PasswordSignInAsync(checkUser, user.Password, false, lockoutOnFailure: true);
            if (checkUser.Status == UserStatus.Lock)
                return AppRespone<object>.Failed("Tài khoản đã bị khóa!");
            else if(result.Succeeded)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                    new Claim(ClaimsTypes.UserId, checkUser.Id.ToString()),
                    new Claim(ClaimsTypes.UserName, checkUser.UserName),
                    new Claim(ClaimsTypes.Avatar, checkUser.Avatar)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var SignIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: SignIn);
                ResponseUser response = new ResponseUser();
                response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                response.FullName = checkUser.FullName;
                response.Avatar = checkUser.Avatar;
                response.Email = checkUser.Email;
                response.UserId = checkUser.Id;
                return AppRespone<object>.Success(response);
            }
            else
            {
                return AppRespone<object>.Failed("Đăng nhập không thành công!");
            }
        }

        [HttpPost("login-admin")]
        public async Task<AppRespone<object>> LoginAdmin(RequestLogin user)
        {
            var checkUser = await _userManager.FindByNameAsync(user.Email);
            if (checkUser == null)
                return AppRespone<object>.Failed("Tên đăng nhập hoặc mật khẩu không chính xác!");
            if (checkUser.IsAdmin != true)
                return AppRespone<object>.Failed("Tên đăng nhập hoặc mật khẩu không chính xác!");
            var result = await _signInManager.PasswordSignInAsync(checkUser, user.Password, false, lockoutOnFailure: true);
            if (checkUser.Status == UserStatus.Lock)
                return AppRespone<object>.Failed("Tài khoản đã bị khóa!");
            else if (result.Succeeded)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                    new Claim(ClaimsTypes.UserId, checkUser.Id.ToString()),
                    new Claim(ClaimsTypes.UserName, checkUser.UserName),
                    new Claim(ClaimsTypes.Avatar, checkUser.Avatar)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var SignIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: SignIn);
                ResponseUser response = new ResponseUser();
                response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                response.FullName = checkUser.FullName;
                response.Avatar = checkUser.Avatar;
                response.Email = checkUser.Email;
                return AppRespone<object>.Success(response);
            }
            else
            {
                return AppRespone<object>.Failed("Đăng nhập không thành công!");
            }
        }

        [HttpPatch("forget-password")]
        public async Task<AppRespone<object>> ForgetPassword(ForgetPasswordRequest data)
        {
            var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                return AppRespone<object>.Failed("Người dùng không tồn tại!");
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, data.NewPassword);
            if (passwordChangeResult.Succeeded)
                return AppRespone<object>.Success("Thay đổi mật khẩu thành công!");
            return AppRespone<object>.Failed("Thay đổi mật khẩu không thành công!");
        }

        [HttpGet("get-users")]
        public async Task<AppRespone<object>> GetUsers(string keyword, UserType type = 0, UserStatus status = 0, int pageindex = 0)
        {
            try
            {
                var result = _DbContext.Users
                    .Where(delegate (UserEntity x)
                    {
                        return (x.FullName.Like(keyword)
                        || x.Email.Like(keyword))
                        && (x.Type == type || type == 0)
                        && (x.Status == status || status == 0);
                    })
                    .OrderByDescending(x => Convert.ToDateTime(x.CreateDate))
                    .Skip(12 * pageindex)
                    .Take(12)
                    .Select(x => new
                    {
                        x.Id,
                        x.Avatar,
                        x.FullName,
                        x.Email,
                        x.PhoneNumber,
                        x.Type,
                        x.Status,
                        x.Address,
                        x.IsAdmin
                    }).ToList();
                return AppRespone<object>.Success(result);
            }
            catch(Exception ex)
            {
                return AppRespone<object>.Failed("Lấy danh sách người dùng thất bại!");
            }
        }

        [HttpGet("get-userinfo")]
        public async Task<AppRespone<object>> GetUserInfo()
        {
            try
            {
                var userinfo = _DbContext.Users
                    .Where(x => x.Id == CurrentUser)
                    .Select(x => new ResponeUserInfo(x, _DbContext))
                    .SingleOrDefault();
                if (userinfo == null)
                    return AppRespone<object>.Failed("Người dùng không tồn tại!");
                return AppRespone<object>.Success(userinfo);
            }
            catch(Exception ex)
            {
                return AppRespone<object>.Failed("Lỗi, không thể lấy thông tin cá nhân!");
            }
        }

        [HttpPut("update-userinfo")]
        public async Task<AppRespone<object>> UpdateUserInfo(RegisterRequest data)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(CurrentUser.ToString());
                if (user == null)
                    return AppRespone<object>.Failed("Cập nhật thông tin người dùng thất bại!");
                user.Avatar = data.AvatarPath;
                user.FullName = data.FullName;
                user.PhoneNumber = data.PhoneNumber;
                user.Address = data.Address;
                user.UpdateUser = CurrentUser;
                user.UpdateTime = DateTime.Now;
                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success("Cập nhật thông tin người dùng thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Cập nhật thông tin người dùng thất bại!");
            }
        }

        [HttpPut("update-user/{id}")]
        public async Task<AppRespone<object>> UpdateUser(int id, RegisterRequest data)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if(user == null)
                    return AppRespone<object>.Failed("Cập nhật thông tin người dùng thất bại!");
                user.Avatar = data.AvatarPath;
                user.FullName = data.FullName;
                user.PhoneNumber = data.PhoneNumber;
                user.Address = data.Address;
                user.UpdateUser = CurrentUser;
                user.UpdateTime = DateTime.Now;
                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success("Cập nhật thông tin người dùng thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Cập nhật thông tin người dùng thất bại!");
            }
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<AppRespone<object>> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return AppRespone<object>.Failed("Xóa người dùng không thành công!");
            user.IsDeleted = true;
            user.UpdateTime = DateTime.Now;
            user.UpdateUser = CurrentUser;
            await _DbContext.SaveChangesAsync();
            return AppRespone<object>.Success("Xóa người dùng thành công!");
        }

        [HttpPatch("lock-user/{id}")]
        public async Task<AppRespone<object>> LockoutUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return AppRespone<object>.Failed("Khóa tài khoản người dùng không thành công!");
            if (user.Id == CurrentUser)
                return AppRespone<object>.Failed("Bạn không thể khóa tài khoản đang được sử dụng!");
            if (user.EmailConfirmed == false)
                return AppRespone<object>.Failed("Tài khoản chưa được xác minh!");
            if (user.Status == UserStatus.Lock)
            {
                user.Status = UserStatus.UnLock;
                user.UpdateUser = CurrentUser;
                user.UpdateTime = DateTime.Now;
                user.Status = UserStatus.UnLock;
                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success("Mở khóa tài khoản người dùng thành công!");
            }
            user.Status = UserStatus.Lock;
            user.LockoutEnd = DateTime.Now;
            user.UpdateUser = CurrentUser;
            user.UpdateTime = DateTime.Now;
            user.Status = UserStatus.Lock;
            await _DbContext.SaveChangesAsync();
            return AppRespone<object>.Success("Khóa tài khoản người dùng thành công!");
        }
        #endregion

        #region Donate Project
        [HttpPost("donate-project")]
        public async Task<AppRespone<object>> DonateProject(int projectid, DonateRequest data)
        {
            try
            {
                var currentProject = _DbContext.Projects.Include(x => x.TransactionHistories).Where(x => x.Id == projectid).SingleOrDefault();
                if (currentProject == null || currentProject.Status == ProjectStatus.Wait)
                    return AppRespone<object>.Failed("Donate không thành công!");

                currentProject.AmountNow += data.Amount;
                var newDonate = new TransactionHistoryEntity
                {
                    UserId = CurrentUser,
                    ProjectId = currentProject.Id,
                    CreateUser = CurrentUser,
                    CreateTime = DateTime.Now,
                    Hash = data.Hash,
                    Currency = "TRX",
                    Amount = data.Amount
                };
                _DbContext.TransactionHistoryEntity.Add(newDonate);
                await _DbContext.SaveChangesAsync();

                //var newDonate = new TransactionHistoryEntity {
                //    UserId = CurrentUser,
                //    CreateUser = CurrentUser,
                //    CreateTime = DateTime.Now,
                //    ProjectId = data.ProjectId,
                //    Amount = data.Amount,
                //    Currency = "TRX",
                //    Hash = data.Hash
                //};
                //_DbContext.TransactionHistoryEntities.Add(newDonate);
                //await _DbContext.SaveChangesAsync();

                return AppRespone<object>.Success("Donate dự án thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Donate không thành công!");
            }
        }

        [HttpPost("refund-donate")]
        public async Task<AppRespone<object>> RefundDonate(int projectid, RefundRequest data)
        {
            try
            {
                //var checkDonation = _DbContext.TransactionHistoryEntity.Where(x => x.ProjectId == projectid && x.UserId == CurrentUser).SingleOrDefault();
                //if (checkDonation == null)
                //    return AppRespone<object>.Failed("Bạn không thể thực thiện hoàn tiền trên dự án này");
                var currentProject = _DbContext.Projects.Include(x => x.TransactionHistories).Where(x => x.Id == projectid).SingleOrDefault();
                if (currentProject == null || currentProject.Status == ProjectStatus.Complete)
                    return AppRespone<object>.Failed("Hoàn tiền không thành công!");

                var result = new TransactionHistoryEntity
                {
                    ProjectId = projectid,
                    UserId = CurrentUser,
                    CreateUser = CurrentUser,
                    CreateTime = DateTime.Now,
                    Amount = data.Amount,
                    Hash = data.Hash,
                    Currency = "TRX",
                    Type = TransactionType.Refund
                };
                _DbContext.TransactionHistoryEntity.Add(result);
                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success("Hoàn tiền dự án thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Hoàn tiền không thành công!");
            }
        }

        //[HttpGet("refund-project")]
        //public async Task<AppRespone<object>> RefundProject(int projectid)
        //{
        //    try
        //    {
        //        var checkUser = await _userManager.FindByIdAsync(CurrentUser.ToString());
        //        if(checkUser == null)
        //            return AppRespone<object>.Failed("Lỗi, không thể thực hiện chức năng hoàn tiền!");
        //        var checkProject = _DbContext.Projects.Where(x => x.Id == projectid).Select(x => x.Title).SingleOrDefault();
        //        if(checkProject == null)
        //            return AppRespone<object>.Failed("Lỗi, không thể thực hiện chức năng hoàn tiền!");
        //        var result = _DbContext.TransactionHistoryEntities.Where(x => x.UserId == CurrentUser && x.ProjectId == projectid).Select(x => x.Amount).ToList();

        //        return AppRespone<object>.Success(result.Sum(), "Hoàn tiền từ dự án này thành công!");
        //    }
        //    catch(Exception ex)
        //    {
        //        return AppRespone<object>.Failed("Lỗi, không thể thực hiện chức năng hoàn tiền!");
        //    }
        //}

        [HttpPost("withdraw-donate")]
        public async Task<AppRespone<object>> WithDrawDonate(int projectid, WithDrawRequest data)
        {
            try
            {
                var currentProject = _DbContext.Projects.Include(x => x.TransactionHistories).Where(x => x.Id == projectid).SingleOrDefault();
                if (currentProject == null || currentProject.Status == ProjectStatus.Implementation)
                    return AppRespone<object>.Failed("Rút tiền không thành công!");

                var result = new TransactionHistoryEntity
                {
                    ProjectId = projectid,
                    UserId = CurrentUser,
                    CreateUser = CurrentUser,
                    CreateTime = DateTime.Now,
                    Amount = data.Amount,
                    Currency = "TRX",
                    Hash = data.Hash,
                    Type = TransactionType.WithDraw
                };
                _DbContext.TransactionHistoryEntity.Add(result);
                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success("Rút tiền từ dự án thành công!");

            }
            catch
            {
                return AppRespone<object>.Failed("Rút tiền từ dự án không thành công!");
            }
        }
        #endregion

        #region Get Transaction History
        [HttpGet("get-user-transaction")]
        public async Task<AppRespone<object>> GetTransaction()
        {
            try
            {
                var result = _DbContext.TransactionHistoryEntity
                    .Include(x => x.Project)
                    .Where(x => x.CreateUser == CurrentUser)
                    .OrderByDescending(x => x.CreateTime)
                    .Select(x => new TransactionRespone(x, _DbContext))
                    .ToList();

                if (result == null)
                    return AppRespone<object>.Failed("Không thể lấy danh sách lịch sử giao dịch!");
                return AppRespone<object>.Success(result, "Thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Không thể lấy danh sách lịch sử giao dịch!");
            }
        }
        #endregion

        #region Follow Project
        [HttpPost("follow-project")]
        public async Task<AppRespone<object>> FollowProject(int projectid)
        {
            try
            {
                var projectFollow = _DbContext.FavoriteProjectEntities.Where(x => x.ProjectId == projectid && x.UserId == CurrentUser && x.IsDeleted == false).SingleOrDefault();
                if(projectFollow != null)
                {
                    projectFollow.IsDeleted = true;
                    projectFollow.UpdateUser = CurrentUser;
                    projectFollow.UpdateTime = DateTime.Now;
                    await _DbContext.SaveChangesAsync();
                    return AppRespone<object>.Success("Đã bỏ theo dõi dự án!");
                }

                var newFollow = new FavoriteProjectEntity
                {
                    UserId = CurrentUser,
                    ProjectId = projectid,
                    CreateTime = DateTime.Now,
                    CreateUser = CurrentUser
                };
                _DbContext.FavoriteProjectEntities.Add(newFollow);
                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success("Dự án đã được thêm vào danh sách theo dõi!");
            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi");
            }
        }
        #endregion
    }
}
