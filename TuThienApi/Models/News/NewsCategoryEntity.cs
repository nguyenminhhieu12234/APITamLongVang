using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;

namespace TuThienApi.Models.News
{
    public class NewsCategoryEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int NewsId { get; set; }
        [ForeignKey("NewsId")]
        public virtual NewsEntity News { get; set; }
        [ForeignKey("CategoryId")]
        public virtual CategoryEntity Category { get; set; }
    }
}
