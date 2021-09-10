using FileUploadAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IOptions<FileSettingModel> _fileSettings;
        public FileController(IHostingEnvironment hostingEnv, IOptions<FileSettingModel> fileSettings)
        {
            _hostingEnv = hostingEnv;
            _fileSettings = fileSettings;
        }

        [HttpPost("upload")]
        public async Task<ActionResult> Fileupload(IFormFile file)
        {
            FileUploadViewModel fileUpload = new FileUploadViewModel();
            //Filesize
            //fileUpload.FileSize = 550;
            fileUpload.FileSize = _fileSettings.Value.FileSize;
            bool result = fileUpload.UploadUserFile(file);

            if (result)
            {
                var uniqueFolderName = Guid.NewGuid().ToString();
                Directory.CreateDirectory(Path.Combine(_hostingEnv.WebRootPath, "UploadFile", uniqueFolderName));              
                var path = Path.Combine(_hostingEnv.WebRootPath, "UploadFile", uniqueFolderName, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            
            return Ok(new JsonResult(fileUpload.Message));
        }
    }
}
