using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;

namespace TuThienApi.Models.Project
{
    public class ProjectCategoryEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }
        [ForeignKey("CategoryId")]
        public virtual CategoryEntity Category { get; set; }
    }
}
