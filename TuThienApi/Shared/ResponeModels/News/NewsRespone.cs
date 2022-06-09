using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.News;
using TuThienApi.Shared.ResponeModels.Category;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.ResponeModels.News
{
    public class NewsRespone
    {
        private readonly ApplicationDbContext _DbContext;


        public NewsRespone() { }

        public NewsRespone(NewsEntity news, ApplicationDbContext DbContext)
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
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FriendlyUrl { get; set; }
        public string Content { get; set; }
        public string BannerPath { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public NewsStatus Status { get; set; }
    }
}
