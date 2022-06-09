using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.Users;

namespace TuThienApi.Shared.RequestModels.User
{
    public class DonateRequest
    {
        public decimal Amount { get; set; }
        public string Hash { get; set; }
    }
}
