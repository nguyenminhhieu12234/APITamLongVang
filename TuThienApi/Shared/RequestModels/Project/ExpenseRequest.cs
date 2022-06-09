using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.RequestModels.Project
{
    public class ExpenseRequest
    {
        public int Id { get; set; } = 0;
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }

        public FileRequest File { get; set; }
    }
}
