using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.News;
using TuThienApi.Models.Project;

namespace TuThienApi.Models.General
{
    public class CategoryEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int FileId { get; set; }
        public int Type { get; set; }

        public virtual FileEntity File { get; set; }
        public virtual ICollection<NewsCategoryEntity> NewsCategories { get; set; }
        public virtual ICollection<ProjectCategoryEntity> ProjectCategories { get; set; }
    }
}
