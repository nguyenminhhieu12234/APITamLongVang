using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;
using TuThienApi.Shared.RequestModels.Category;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.RequestModels.News
{
    public class NewsRequest
    {
        public string Title { get; set; }
        public string ShortDescription{ get; set; }
        public string Content { get; set; }
        public string FriendlyUrl { get; set; }
        public NewsStatus Status { get; set; }
        public BannerNewsRequest Banner { get; set; }
        public List<NewsCategoryRequest> Category { get; set; }
    }
}
