using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.RequestModels.Category
{
    public class RequestCategory
    {
        public string CategoryName { get; set; }
        public RequestIconCategory Icon { get; set; }
        public int Type { get; set; }
    }
}
