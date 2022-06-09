using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;

namespace TuThienApi.Models.Project
{
    public class ExpenseEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public int FileId { get; set; }
        public int ProcessId { get; set; }

        public virtual ProcessEntity Process { get; set; }
        public virtual FileEntity File { get; set; }
    }
}
