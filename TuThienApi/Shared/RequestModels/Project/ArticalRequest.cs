using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.RequestModels.Project
{
    public class ArticalRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string FriendlyUrl { get; set; }
        public FileRequest Banner { get; set; }
    }
}
