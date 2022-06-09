using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;
using TuThienApi.Models.Project;

namespace TuThienApi.Models.Users
{
    public class FavoriteProjectEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }
    }
}
