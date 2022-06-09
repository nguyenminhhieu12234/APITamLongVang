using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Project;

namespace TuThienApi.Shared.ResponeModels.Projects
{
    public class ArticalRespone
    {
        private readonly ApplicationDbContext _DbContext;

        public ArticalRespone(ArticalEntity artical, ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
            ArticalId = artical.Id;
            FriendlyUrl = artical.FriendlyUrl;
            Banner = artical.File.FilePath;
            Title = artical.Title;
            Content = artical.Content;
            CreateTime = artical.CreateTime;
            UserCreate = _DbContext.Users.Where(x => x.Id == artical.CreateUser).Select(x => x.FullName).SingleOrDefault();
        }

        public int ArticalId { get; set; }
        public string Banner { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string FriendlyUrl { get; set; }
        public DateTime CreateTime { get; set; }
        public string UserCreate { get; set; }
    }
}
