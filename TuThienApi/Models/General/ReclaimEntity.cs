using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Models.General
{
    public class ReclaimEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreateUserEmail { get; set; }
        public ReclaimStatus Status { get; set; } = ReclaimStatus.Wait;
    }
}
