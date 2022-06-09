using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;
using TuThienApi.Models.Project;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.RequestModels.Project
{
    public class ProcessRequest
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public decimal AmountNeed { get; set; }
        public ProcessStatus Status { get; set; }

        public IList<ExpenseRequest> Expenses { get; set; }
        public IList<FileRequest> Files { get; set; }
    }
}
