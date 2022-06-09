using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Models.News
{
    public class NewsEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public NewsStatus Status { get; set; } = NewsStatus.Show;
        public string FriendlyUrl { get; set; }

        public virtual FileEntity File { get; set; }
        public virtual ICollection<NewsCategoryEntity> NewsCategories { get; set; }
    }
}
