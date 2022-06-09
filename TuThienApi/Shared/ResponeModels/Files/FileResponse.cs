using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.ResponeModels.Files
{
    public class FileResponse
    {
        public FileResponse(string filename, string filepath, string friendlyurl, string note)
        {
            FileName = filename;
            FilePath = filepath;
            FriendlyUrl = friendlyurl;
            Note = note;
        }

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FriendlyUrl { get; set; }
        public string Note { get; set; }
    }
}
