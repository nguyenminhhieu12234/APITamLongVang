using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.General;
using TuThienApi.Shared.RequestModels.Category;
using TuThienApi.Shared.ResponeModels;
using TuThienApi.Shared.ResponeModels.Category;

namespace TuThienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        public CategoryController(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpPost("create-category")]
        public async Task<AppRespone<object>> CreateCategory(RequestCategory data)
        {
            try
            {
                var result = new CategoryEntity { 
                    CategoryName = data.CategoryName,
                    File = new FileEntity
                    {
                        FileName = data.Icon.FileName,
                        FilePath = data.Icon.FilePath,
                        FriendlyUrl = data.Icon.FriendlyUrl,
                        Note = data.Icon.Note,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser
                    },
                    CreateUser = CurrentUser,
                    CreateTime = DateTime.Now,
                    Type = data.Type
                };
                _DbContext.Categories.Add(result);
                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success("Thêm danh mục thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Thêm danh mục không thành công!");
            }
        }

        [HttpGet("get-categories")]
        public async Task<AppRespone<object>> GetProjectCategories(int type = 0)
        {
            try
            {
                var result = _DbContext.Categories
                    .Include(x => x.File)
                    .Where(c => c.Type == type || type == 0)
                    .Select(c => new {
                        c.Id,
                        c.CategoryName,
                        c.File.FilePath
                    }).ToList();
                return AppRespone<object>.Success(result);
            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi không lấy được danh sách danh mục");
            }
        }

        [HttpGet("get-info-category/{id}")]
        public async Task<AppRespone<object>> GetInfoCategory(int id)
        {
            try
            {
                var result = _DbContext.Categories
                    .Include(x => x.File)
                    .Where(x => x.Id == id)
                    .Select(x => new CategoryRespone(x, _DbContext))
                    .SingleOrDefault();

                return AppRespone<object>.Success(result);
            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi, không thể lấy thông tin danh mục!");
            }
        }

        //[HttpGet("get-news-categories")]
        //public async Task<AppRespone<object>> GetNewsCategories()
        //{
        //    try
        //    {
        //        var result = _DbContext.Categories
        //            .Where(c => c.Type == 2)
        //            .Select(c => new
        //            {
        //                c.Id,
        //                c.CategoryName
        //            }).ToList();

        //        return AppRespone<object>.Success(result);
        //    }
        //    catch
        //    {
        //        return AppRespone<object>.Failed("Lỗi không lấy được danh sách danh mục");
        //    }
        //}

        [HttpPut("update-category/{id}")]
        public async Task<AppRespone<object>> UpdateCategory(int id, RequestCategory data)
        {
            try
            {
                var currentcategory = _DbContext.Categories.Include(x => x.File).Where(x => x.Id == id).SingleOrDefault();
                if (currentcategory == null)
                    return AppRespone<object>.Failed("Lỗi, danh mục không tồn tại!");
                currentcategory.CategoryName = data.CategoryName;
                currentcategory.Type = data.Type;
                currentcategory.UpdateTime = DateTime.Now;
                currentcategory.UpdateUser = CurrentUser;
                currentcategory.File.FileName = data.Icon.FileName;
                currentcategory.File.FilePath = data.Icon.FilePath;
                currentcategory.File.FriendlyUrl = data.Icon.FriendlyUrl;
                currentcategory.File.Note = data.Icon.Note;

                await _DbContext.SaveChangesAsync();
                return AppRespone<object>.Success("Cập nhật danh mục thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi, cập nhật danh mục không thành công!");
            }
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<AppRespone<object>> DeleteCategory(int id)
        {
            try
            {
                var currentcategory = _DbContext.Categories.Include(x => x.File).Where(x => x.Id == id).SingleOrDefault();
                if (currentcategory == null)
                    return AppRespone<object>.Failed("Danh mục không tồn tại!");
                currentcategory.IsDeleted = true;
                await _DbContext.SaveChangesAsync();

                return AppRespone<object>.Success("Xóa danh mục thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Lỗi, Xóa danh mục không thành công!");
            }
        }
    }
}
