using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.RequestModels.Project
{
    public class FileRequest
    {
        public int Id { get; set; } = 0;
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FriendlyUrl { get; set; }
        public string Note { get; set; }
    }
}
