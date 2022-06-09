using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.News;
using TuThienApi.Models.Project;

namespace TuThienApi.Models.General
{
    public class FileEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FriendlyUrl { get; set; }
        public string Note { get; set; }
        public int ProcessId { get; set; }
        public int ProjectId { get; set; }
        public int NewsId { get; set; }
        public int ExpenseId { get; set; }
        public int ArticalId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("ProcessId")]
        public virtual ProcessEntity Process { get; set; }

        [ForeignKey("ExpenseId")]
        public virtual ExpenseEntity Expense { get; set; }

        [ForeignKey("ProjectId")]
        public virtual ProjectEntity Project { get; set; }

        [ForeignKey("ArticalId")]
        public virtual ArticalEntity Artical { get; set; }

        [ForeignKey("NewsId")]
        public virtual NewsEntity News { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CategoryEntity Category { get; set; }
    }
}
