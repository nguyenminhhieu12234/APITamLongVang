using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.RequestModels.System
{
    public class SystemRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
        public string Medium { get; set; }
    }
}
