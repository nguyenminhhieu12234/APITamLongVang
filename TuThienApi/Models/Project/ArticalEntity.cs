using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;

namespace TuThienApi.Models.Project
{
    public class ArticalEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string FriendlyUrl { get; set; }
        public string Content { get; set; }
        public int ProjectId { get; set; }
        public int FileId { get; set; }

        public virtual ProjectEntity Project { get; set; }
        public virtual FileEntity File { get; set; }
    }
}
