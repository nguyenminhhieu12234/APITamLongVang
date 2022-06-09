using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Project;
using TuThienApi.Shared.ResponeModels.Files;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.ResponeModels.Projects
{
    public class ProcessRespone
    {
        private readonly ApplicationDbContext _DbContext;

        public ProcessRespone(ProcessEntity process, ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
            ProcessId = process.Id;
            Title = process.Title;
            Status = process.Status;
            ShortDescription = process.ShortDescription;
            Content = process.Content;
            AmountNeed = process.AmountNeed;
            ListImages = process.Files.Select(f => new FileProcessResponse(f)).ToList();
            Expenses = process.Expenses.Select(ex => new ExpenseResponse(ex)).ToList();
        }

        public int ProcessId { get; set; }
        public string Title { get; set; }
        public ProcessStatus Status { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public decimal AmountNeed { get; set; }
        public List<FileProcessResponse> ListImages { get; set; }
        public List<ExpenseResponse> Expenses { get; set; }

    }
}
