using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.RequestModels.Files
{
    public class UploadImage
    {
        public UploadImage()
        {

        }

        public IFormFile Image { get; set; }
        public String TypeImage { get; set; }
    }
}
