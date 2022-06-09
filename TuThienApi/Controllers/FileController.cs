using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TuThienApi.Shared.Extensions;
using TuThienApi.Shared.RequestModels.Files;
using TuThienApi.Shared.ResponeModels;
using TuThienApi.Shared.ResponeModels.Files;

namespace TuThienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseApiController
    {
        #region Variables
        AppGeneral app = new AppGeneral();
        #endregion


        public FileController(IWebHostEnvironment WebHostEnviroment)
        {
            _WebHostEnviroment = WebHostEnviroment;
        }

        #region Upload Image
        [HttpPost("upload-image")]
        public async Task<AppRespone<object>> UploadImage([FromForm] UploadImage newFile)
        {
            try
            {
                if(newFile.Image.Length > 0)
                {
                    string path = "";
                    string typeFolder = "";
                    var fileName = DateTime.Now.ToString("ddMMyyyy_hhmmss_") + newFile.Image.FileName;
                    string typeFile = app.TypeFileUpload(fileName);
                    if (typeFile == ".jpg" || typeFile == ".png" || typeFile == "jepg")
                    {
                        typeFolder = "Images";
                        path = _WebHostEnviroment.WebRootPath + $"\\uploads\\{typeFolder}\\{newFile.TypeImage}\\";
                    }
                    else if(typeFile == ".pdf" || typeFile == ".docx")
                    {
                        typeFolder = "Files";
                        path = _WebHostEnviroment.WebRootPath + $"\\uploads\\{typeFolder}\\{newFile.TypeImage}\\";
                    }
                    else
                    {
                        return AppRespone<object>.Failed("Định dạnh không hỗ trợ!");
                    }

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + fileName))
                    {
                        await newFile.Image.CopyToAsync(fileStream);
                        fileStream.Flush();
                        FileResponse response = new FileResponse(fileName, $"\\uploads\\{typeFolder}\\{newFile.TypeImage}\\{fileName}",fileName,"Banner Project");
                        return AppRespone<object>.Success(response);
                    }
                }
                else
                {
                    return AppRespone<object>.Failed("Tải hình ảnh lên thất bại");
                }
            }
            catch(Exception ex)
            {
                return AppRespone<object>.Failed(ex.Message);
            }
        }
        #endregion
    }
}
