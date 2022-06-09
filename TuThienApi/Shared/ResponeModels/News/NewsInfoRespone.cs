using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.News;
using TuThienApi.Shared.ResponeModels.Category;

namespace TuThienApi.Shared.ResponeModels.News
{
    public class NewsInfoRespone:NewsRespone
    {
        private readonly ApplicationDbContext _DbContext;

        public NewsInfoRespone(NewsEntity news, ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
            Id = news.Id;
            Title = news.Title;
            ShortDescription = news.ShortDescription;
            FriendlyUrl = news.FriendlyUrl;
            Content = news.Content;
            CreateTime = news.CreateTime;
            CreateUser = _DbContext.Users.Where(x => x.Id == news.CreateUser).Select(x => x.FullName).SingleOrDefault();
            BannerPath = news.File.FilePath;
            Status = news.Status;
            Category = news.NewsCategories.Select(x => x.Category).ToList().Select(x => new CategoryRespone(x, _DbContext)).ToList();
        }

        public List<CategoryRespone> Category { get; set; }
    }
}
