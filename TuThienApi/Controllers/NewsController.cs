using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.General;
using TuThienApi.Models.News;
using TuThienApi.Shared.Extensions;
using TuThienApi.Shared.RequestModels.News;
using TuThienApi.Shared.ResponeModels;
using TuThienApi.Shared.ResponeModels.News;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : BaseApiController
    {
        public NewsController(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        #region News
        [HttpPost("create-news")]
        public async Task<AppRespone<object>> CreateNews(NewsRequest data)
        {
            try
            {
                var result = new NewsEntity
                {
                    Title = data.Title,
                    ShortDescription = data.ShortDescription,
                    Content = data.Content,
                    FriendlyUrl = data.FriendlyUrl,
                    File = new FileEntity
                    {
                        FileName = data.Banner.FileName,
                        FilePath = data.Banner.FilePath,
                        FriendlyUrl = data.Banner.FriendlyUrl,
                        Note = data.Banner.Note,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser
                    },
                    //Them danh muc cho tin tuc
                    NewsCategories = data.Category.Select(x => new NewsCategoryEntity(){
                        CategoryId = x.CategoryId,
                        CreateTime = DateTime.Now,
                        CreateUser = CurrentUser
                    }).ToList(),
                    CreateTime = DateTime.Now,
                    CreateUser = CurrentUser
                };
                _DbContext.News.Add(result);
                await _DbContext.SaveChangesAsync();

                return AppRespone<object>.Success("Tạo tin tức thành công!");
            }
            catch
            {
                return AppRespone<object>.Success();
            }
        }

        [HttpGet("get-news")]
        public async Task<AppRespone<object>> GetNews(string keyword, int categoryid, int pageindex = 0, NewsStatus status = 0)
        {
            try
            {
                var listNews = _DbContext.News
                    .Include(x => x.File)
                    .Include(x => x.NewsCategories).ThenInclude(x => x.Category)
                    .Where(delegate (NewsEntity x) {
                        return (x.Title.Like(keyword))
                        && (x.Status == status || status == 0)
                        && (x.NewsCategories.Any(x => x.CategoryId == categoryid || categoryid == 0));
                    })
                    .OrderByDescending(x => Convert.ToDateTime(x.CreateTime))
                    .Skip(pageindex * 12)
                    .Take(12)
                    .Select(x => new NewsRespone(x, _DbContext))
                    .ToList();
                return AppRespone<object>.Success(listNews);
            }
            catch
            {
                return AppRespone<object>.Failed("Lấy danh sách tin tức không thành công!");
            }
        }

        [HttpGet("get-newsinfo/{id}")]
        public async Task<AppRespone<object>> GetNewsInfo(int id)
        {
            try
            {
                var result = _DbContext.News
                    .Include(x => x.File)
                    .Include(x => x.NewsCategories).ThenInclude(x => x.Category)
                    .Where(x => x.Id == id)
                    .Select(x => new NewsInfoRespone(x, _DbContext))
                    .SingleOrDefault();
                if(result == null)
                    return AppRespone<object>.Failed("Lỗi, không thể lấy thông tin tin tức!");
                return AppRespone<object>.Success(result);
            }
            catch(Exception ex)
            {
                return AppRespone<object>.Failed("Lỗi, không thể lấy thông tin tin tức!");
                //return AppRespone<object>.Failed(ex.Message);
            }
        }

        [HttpPut("update-news/{id}")]
        public async Task<AppRespone<object>> UpdateNews(int id, NewsRequest data)
        {
            try
            {
                var currentNews = _DbContext.News
                    .Include(x => x.File)
                    .Include(x => x.NewsCategories)
                    .Where(x => x.Id == id).SingleOrDefault();
                if (currentNews == null)
                    return AppRespone<object>.Failed("Tin tức không tồn tại!");
                currentNews.Title = data.Title;
                currentNews.ShortDescription = data.ShortDescription;
                currentNews.FriendlyUrl = data.FriendlyUrl;
                currentNews.Status = data.Status;
                currentNews.Content = data.Content;
                currentNews.UpdateUser = CurrentUser;
                currentNews.UpdateTime = DateTime.Now;
                currentNews.File.FileName = data.Banner.FileName;
                currentNews.File.FilePath = data.Banner.FilePath;
                currentNews.File.FriendlyUrl = data.Banner.FriendlyUrl;
                currentNews.File.Note = data.Banner.Note;
                currentNews.File.UpdateTime = DateTime.Now;
                currentNews.File.UpdateUser = CurrentUser;

                //Xóa category không tồn tại trong category mới cần update
                var deletecategory = currentNews.NewsCategories.Where(x => x.NewsId == id && !data.Category.Select(x => x.CategoryId).Contains(x.CategoryId)).ToList();
                foreach(var item in deletecategory)
                {
                    item.IsDeleted = true;
                }

                //Thêm category mới
                var newcategory = data.Category.Where(x => !currentNews.NewsCategories.Select(x => x.CategoryId).Contains(x.CategoryId)).ToList();
                _DbContext.NewsCategories.AddRange(newcategory.Select(x => new NewsCategoryEntity() {
                    CategoryId = x.CategoryId,
                    CreateTime = DateTime.Now,
                    CreateUser = CurrentUser,
                    NewsId = id
                }));

                await _DbContext.SaveChangesAsync();

                return AppRespone<object>.Success("Cập nhật tin tức thành công!");
            }
            catch
            {
                return AppRespone<object>.Failed("Cập nhật tin tức không thành công!");
            }
        }

        [HttpDelete("delete-news/{id}")]
        public async Task<AppRespone<object>> DeleteNews(int id)
        {
            var result = _DbContext.News.Where(x => x.Id == id).SingleOrDefault();
            if (result == null)
                return AppRespone<object>.Failed("Tin tức không tồn tại!");
            result.IsDeleted = true;
            await _DbContext.SaveChangesAsync();
            return AppRespone<object>.Success("Xóa tin tức thành công!");
        }
        #endregion
    }
}
