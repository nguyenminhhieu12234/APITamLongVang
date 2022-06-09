using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.Project;

namespace TuThienApi.Shared.ResponeModels.Projects
{
    public class ExpenseResponse
    {
        public ExpenseResponse(ExpenseEntity expense)
        {
            ExpenseId = expense.Id;
            Description = expense.Description;
            Amount = expense.Amount;
            CreateTime = expense.CreateTime;

            File = expense.File?.FilePath?.ToString();
        }

        public int ExpenseId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateTime { get; set; }

        //file bằng chứng 
        public string File { get; set; }
    }
}
