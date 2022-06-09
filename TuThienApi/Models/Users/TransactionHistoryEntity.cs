using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;
using TuThienApi.Models.Project;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Models.Users
{
    [Table("TransactionHistory")]
    public class TransactionHistoryEntity : BaseEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Hash { get; set; }
        public TransactionType Type { get; set; } = TransactionType.Donate;
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }
    }
}
