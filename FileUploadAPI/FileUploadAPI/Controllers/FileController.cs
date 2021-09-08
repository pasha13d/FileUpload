using FileUploadAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public FileController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        [HttpPost("upload")]
        public async Task<ActionResult> Fileupload(IFormFile file)
        {
            FileUploadViewModel fileUpload = new FileUploadViewModel();
            //Filesize
            fileUpload.FileSize = 550;
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
