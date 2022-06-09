using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Models.General
{
    public class EmailContentEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int SystemConfigId { get; set; }

        public virtual SystemConfigEntity SystemConfig { get; set; }
    }
}
