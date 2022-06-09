using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Models.Project
{
    public class ProcessEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public ProcessStatus Status { get; set; } = ProcessStatus.NotStarted;
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public int ProjectId { get; set; }
        public decimal AmountNeed { get; set; }

        public virtual ProjectEntity Project { get; set; }
        public virtual ICollection<ExpenseEntity> Expenses { get; set; }
        public virtual List<FileEntity> Files { get; set; }
    }
}
