using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.RequestModels.System
{
    public class EmailContentRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
