using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.RequestModels.User
{
    public class RefundRequest
    {
        //public string UserId { get; set; }
        //public int ProjectId { get; set; }

        public decimal Amount { get; set; }
        public string Hash { get; set; }
    }
}
