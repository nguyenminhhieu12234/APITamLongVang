using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.General;
using TuThienApi.Models.Project;
using TuThienApi.Models.Users;
using TuThienApi.Shared;
using TuThienApi.Shared.Extensions;
using TuThienApi.Shared.Global;
using TuThienApi.Shared.Models;
using TuThienApi.Shared.RequestModels.Project;
using TuThienApi.Shared.ResponeModels;
using TuThienApi.Shared.ResponeModels.Projects;
using TuThienApi.Shared.ResponeModels.TransactionHistory;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseApiController
    {
        public ProjectController(UserManager<UserEntity> userManager, IConfiguration configuration
            , SignInManager<UserEntity> signInManager, ApplicationDbContext DbContext, IWebHostEnvironment WebHostEnviroment)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _DbContext = DbContext;
            _WebHostEnviroment = WebHostEnviroment;
        }

        #region Project
        [HttpPost("create-project")]
        public async Task<AppRespone<object>> CreateProject(ProjectRequest newProject)
        {
            try
            {
                var result = new ProjectEntity
                {
                    Title = newProject.Title,
                    ShortDescription = newProject.ShortDescription,
                    FriendlyUrl = newProject.FriendlyUrl,
                    Summary = newProject.Summary,
                    ProblemToAddress = newProject.ProblemToAddress,
                    Solution = newProject.Solution,
                    Location = newProject.Location,
                    Impact = newProject.Impact,
                    EndDate = newProject.EndDate,
                    AddressContract = newProject.AddressContract,
                    Currency = "TRX",
                    AmountNeed = newProject.AmountNeed,
                    AmountTRXNeed = newProject.AmountTRXNeed,
                    CreateTime = DateTime.Now,
                    CreateUser = CurrentUser,
                    UserId = CurrentUser,
                    //Banner cho project
                    File = new FileEntity {
                        FileName = newProject.Banner.FileName,
                        FilePath = newProject.Banner.FilePath,
                        FriendlyUrl = newProject.Banner.FriendlyUrl,
                        Note = newProject.Banner.Note,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser
                    },
                    //Danh mục cho project
                    ProjectCategories = newProject.Category.Select(c => new ProjectCategoryEntity() {
                        CategoryId = c.CategoryId,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser
                    }).ToList(),
                    //Tiến trình của project
                    Processes = newProject.Process.Select(p => new ProcessEntity() { 
                        Title = p.Title,
                        ShortDescription = p.ShortDescription,
                        Content = p.Content,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser,
                        AmountNeed = p.AmountNeed
                    }).ToList()
                };

                _DbContext.Projects.Add(result);
                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success(result.Id, "Tạo dự án thành công, dự án sẽ mất thời gian để xác minh và chúng tôi sẽ gửi thông báo đến email của bạn khi dự án được xác minh, vui lòng kiểm tra email!");
            }
            catch
            {
                return AppRespone<object>.Failed("Tạo dự án không thành công!");
            }
        }

        [HttpGet("get-project")]
        public async Task<AppRespone<object>> GetProject(string keyword, int categoryid = 0, ProjectStatus status = 0, int currentpage = 0)
        {
            try
            {

                var result = _DbContext.Projects
                    .Include(c => c.ProjectCategories).ThenInclude(c => c.Category)
                    .Include(u => u.User)
                    .Include(f => f.File)
                    .Where(delegate (ProjectEntity p) {
                        return (p.Title.Like(keyword))
                        && (p.ProjectCategories.Any(c => c.CategoryId == categoryid || categoryid == 0))
                        && (p.Status == status || status == 0);
                    })
                    .OrderByDescending(p => Convert.ToDateTime(p.CreateTime))
                    .Skip(currentpage * 12)
                    .Take(12)
                    .Select(p => new ProjectRespone(p))
                    .ToList();

                return AppRespone<object>.Success(result);
            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi không thể lấy danh sách dự án!");
            }
        }

        [HttpGet("get-ownerproject")]
        public async Task<AppRespone<object>> GetOwnerProject(int currentpage = 0)
        {
            try
            {
                var result = _DbContext.Projects
                    //.Include(c => c.ProjectCategories).ThenInclude(c => c.Category)
                    .Include(u => u.User)
                    .Include(f => f.File)
                    .Where(p => p.CreateUser == CurrentUser)
                    .OrderByDescending(p => p.CreateTime)
                    .Skip(currentpage * 10)
                    .Take(10)
                    .Select(p => new ProjectRespone(p))
                    .ToList();

                return AppRespone<object>.Success(result);
            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi không thể lấy danh sách dự án!");
            }
        }

        [HttpGet("get-project/{id}")]
        public async Task<AppRespone<object>> GetProjectId(int id)
        {
            try
            {
                //lay ra chi tiet project theo class detailprojectrespone
                var result = _DbContext.Projects
                    .Include(c => c.ProjectCategories).ThenInclude(c => c.Category)
                    .Include(u => u.User)
                    .Include(f => f.File)
                    .Include(process => process.Processes).ThenInclude(process => process.Files)
                    .Include(process => process.Processes).ThenInclude(process => process.Expenses).ThenInclude(process => process.File)
                    .Include(artical => artical.Articals)
                    .Where(p => p.Id == id)
                    .Select(p => new DetailProjectRespone(p, _DbContext, CurrentUser))
                    .SingleOrDefault();
                return AppRespone<object>.Success(result);
            }
            catch(Exception ex)
            {
                return AppRespone<object>.Failed(ex.Message);
            }
        }

        [HttpPut("update-project/{id}")]
        public async Task<AppRespone<object>> UpdateProject(int id, ProjectUpdateRequest data)
        {
            try
            {
                var project = _DbContext.Projects.Include(x => x.ProjectCategories).Where(x => x.Id == id).SingleOrDefault();
                if(project != null)
                {
                    project.Title = data.Title;
                    project.Solution = data.Solution;
                    project.Status = data.ProjectStatus;
                    project.Summary = data.Summary;
                    project.ShortDescription = data.ShortDescription;
                    project.ProblemToAddress = data.ProblemToAddress;
                    project.Location = data.Location;
                    project.Impact = data.Impact;
                    project.UpdateTime = DateTime.Now;
                    project.UpdateUser = CurrentUser;
                    project.FriendlyUrl = data.FriendlyUrl;
                    project.File = new FileEntity
                    {
                        FileName = data.Banner.FileName,
                        FilePath = data.Banner.FilePath,
                        FriendlyUrl = data.Banner.FriendlyUrl,
                        Note = data.Banner.Note,
                        CreateUser = CurrentUser,
                        CreateTime = DateTime.Now
                    };
                    _DbContext.Files.Add(project.File);
                    // Cập nhật lại category
                    var removedCatetory = project.ProjectCategories.Where(x => !data.Category.Select(x => x.CategoryId).Contains(x.CategoryId)).ToList();
                    foreach(var category in removedCatetory)
                    {
                        category.IsDeleted = true;
                    }

                    //Thêm các category mới sau update vào db.
                    var addNewCategory = data.Category.Where(x => !project.ProjectCategories.Select(x => x.CategoryId).Contains(x.CategoryId)).ToList();
                    var newCategory = addNewCategory.Select(x => new ProjectCategoryEntity() {
                        CategoryId = x.CategoryId,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser,
                        ProjectId = id
                    }).ToList();
                    _DbContext.ProjectCategories.AddRange(newCategory);

                    await _DbContext.SaveChangesAsync();
                    return AppRespone<object>.Success("Cập nhật dự án thành công!");
                }
                else
                {
                    return AppRespone<object>.Failed("Lỗi cập nhật không thành công!");
                }
            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi cập nhật không thành công!");
            }
        }

        [HttpPatch("update-status-project/{id}")]
        public async Task<AppRespone<object>> UpdateStatusProject(int id, bool iscancelled = false)
        {
            var project = _DbContext.Projects.Include(x => x.User).Where(x => x.Id == id).SingleOrDefault();
            if (project == null)
                return AppRespone<object>.Failed("Dự án không tồn tại!");
            if (project.Status == ProjectStatus.Wait)
            {
                project.Status = ProjectStatus.Implementation;
                var mailConfig = _DbContext.SystemConfigs.FirstOrDefault();
                var bodyEmail = System.IO.File.ReadAllText(_WebHostEnviroment.WebRootPath + $"\\email\\BodyEmail\\run_project_email.html");
                bodyEmail = bodyEmail.Replace("EmailUserRegister", project.User.Email);
                bodyEmail = bodyEmail.Replace("ProjectName", project.Title);
                bodyEmail = bodyEmail.Replace("AmountNeed", project.AmountNeed.ToString());
                bodyEmail = bodyEmail.Replace("idProject", project.Id.ToString());
                bodyEmail = bodyEmail.Replace("friendlyUrl", project.FriendlyUrl);
                if (Global.SendEmail(project.User.Email, "Dự án đã được xác minh", bodyEmail, mailConfig.Email, mailConfig.EmailPassword, "smtp.gmail.com", 587, true))
                {
                    await _DbContext.SaveChangesAsync();
                    return AppRespone<object>.Success("Dự án xác minh thành công!");
                }
                return AppRespone<object>.Failed("Xác minh dự án không thành công!");
                    
            }
            else
            {
                return AppRespone<object>.Failed("Dự án này không thể xác minh!");
            }
        }
        #endregion

        #region Expense
        [HttpPut("update-process/{id}")]
        public async Task<AppRespone<object>> UpdateProcess(int id, ProcessRequest data)
        {
            try
            {
                var currentProcess = _DbContext.Processes
                    .Include(process => process.Files)
                    .Include(process => process.Expenses)
                    .Where(process => process.Id == id).SingleOrDefault();
                if(currentProcess != null)
                {
                    currentProcess.Title = data.Title;
                    currentProcess.ShortDescription = data.ShortDescription;
                    currentProcess.Content = data.Content;
                    currentProcess.Status = data.Status;

                    //Xóa file
                    var removedFiles = currentProcess.Files.Where(f => !data.Files.Select(a => a.Id).Contains(f.Id)).ToList();
                    foreach(var file in removedFiles)
                    {
                        file.IsDeleted = true;
                    }
                    //Thêm file mới
                    var newFiles = data.Files.Where(f => !currentProcess.Files.Select(f => f.Id).Contains(f.Id)).ToList();
                    var addNewFiles = newFiles.Select(f => new FileEntity() {
                        FileName = f.FileName,
                        FilePath = f.FilePath,
                        FriendlyUrl = f.FriendlyUrl,
                        Note = f.Note,
                        ProcessId = id,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser
                    }).ToList();
                    _DbContext.Files.AddRange(addNewFiles);

                    //Xóa expense
                    var removeExpense = currentProcess.Expenses.Where(ex => !data.Expenses.Select(a => a.Id).Contains(ex.Id)).ToList();
                    foreach(var expense in removeExpense)
                    {
                        expense.IsDeleted = true;
                    }

                    //Thêm mới Expense
                    var newExpense = data.Expenses.Where(ex => !currentProcess.Expenses.Select(ex => ex.Id).Contains(ex.Id)).ToList();
                    var addNewExpense = newExpense.Select(ex => new ExpenseEntity() {
                        Description = ex.Description,
                        Type = ex.Type,
                        Amount = ex.Amount,
                        ProcessId = id,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser,
                        File = new FileEntity
                        {
                            FileName = ex.File.FileName,
                            FilePath = ex.File.FilePath,
                            FriendlyUrl = ex.File.FriendlyUrl,
                            Note = ex.File.Note,
                            CreateTime = DateTime.Now,
                            CreateUser = CurrentUser
                        }
                        
                    }).ToList();
                    _DbContext.Expenses.AddRange(addNewExpense);

                    await _DbContext.SaveChangesAsync();
                    return AppRespone<object>.Success("Thêm chi phí thành công!");
                }
                else
                {
                    return AppRespone<object>.Failed("Lỗi không thể thêm chi phí!");
                }

            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi không thể thêm chi phí!");
            }
        }
        #endregion

        #region Artical
        [HttpPost("create-artical")]
        public async Task<AppRespone<object>> CreateArtical(int projectid, ArticalRequest data)
        {
            try
            {
                var newArtical = new ArticalEntity
                {
                    Title = data.Title,
                    Content = data.Content,
                    ProjectId = projectid,
                    CreateUser = CurrentUser,
                    CreateTime = DateTime.Now,
                    FriendlyUrl = data.FriendlyUrl,
                    File = new FileEntity
                    {
                        FileName = data.Banner.FileName,
                        FilePath = data.Banner.FilePath,
                        FriendlyUrl = data.Banner.FriendlyUrl,
                        Note = data.Banner.Note,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser
                    }
                };
                _DbContext.Articals.Add(newArtical);
                await _DbContext.SaveChangesAsync();

                return AppRespone<object>.Success("Thêm bài viết thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Tạo bài viết thất bại!");
            }
        }

        [HttpGet("get-artical/{id}")]
        public async Task<AppRespone<object>> GetArtical(int id)
        {
            try
            {
                var result = _DbContext.Articals.Include(x => x.File).Where(x => x.Id == id).Select(x => new ArticalRespone(x, _DbContext)).SingleOrDefault();
                return AppRespone<object>.Success(result);
            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi, lấy thông tin bài viết thất bại!");
            }
        }

        [HttpPut("update-artical/{id}")]
        public async Task<AppRespone<object>> UpdateArtical(int id, ArticalRequest data)
        {
            try
            {
                var currentArtical = _DbContext.Articals.Include(x => x.File).Where(x => x.Id == id).SingleOrDefault();
                if (currentArtical == null)
                    return AppRespone<object>.Failed("Cập nhật bài viết thất bại!");
                currentArtical.Title = data.Title;
                currentArtical.Content = data.Content;
                currentArtical.UpdateUser = CurrentUser;
                currentArtical.UpdateTime = DateTime.Now;
                currentArtical.FriendlyUrl = data.FriendlyUrl;
                currentArtical.File.FileName = data.Banner.FileName;
                currentArtical.File.FilePath = data.Banner.FilePath;
                currentArtical.File.FriendlyUrl = data.Banner.FriendlyUrl;
                currentArtical.File.Note = data.Banner.Note;
                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success("Cập nhật bài viết thành công!");
                                                
            }
            catch
            {
                return AppRespone<object>.Failed("Cập nhật bài viết thất bại!");
            }
        }
        #endregion

        #region Transaction History
        [HttpGet("get-transactions")]
        public async Task<AppRespone<object>> GetTransactions(int projectid, int pageindex = 0)
        {
            try
            {
                var transactions = _DbContext.TransactionHistoryEntity
                    .Include(x => x.Project)
                    .Where(x => x.ProjectId == projectid)
                    .OrderByDescending(x => x.CreateTime)
                    .Skip(pageindex * 12)
                    .Take(12)
                    .Select(x => new TransactionRespone(x, _DbContext))
                    .ToList();
                return AppRespone<object>.Success(transactions);
            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi, không thể lấy danh sách lịch sử giao dịch của dự án!");
            }
        }
        #endregion
    }
}
