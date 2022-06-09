using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;

namespace TuThienApi.Shared.ResponeModels.Files
{
    public class FileProcessResponse
    {
        public FileProcessResponse(FileEntity file)
        {
            FileId = file.Id;
            FilePath = file.FilePath;
        }

        public int FileId { get; set; }
        public string FilePath { get; set; }
    }
}
