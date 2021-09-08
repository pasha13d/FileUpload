using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadAPI.Models
{
    public class FileUploadViewModel
    {
        public string Message { get; set; }
        public decimal FileSize { get; set; }
        public bool UploadUserFile([FromForm] IFormFile file)
        {
            bool result = false;
            try
            {
                bool isValidFileType = string.Equals(file.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(file.ContentType, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(file.ContentType, "application/vnd.ms-excel", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(file.ContentType, "text/plain", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(file.ContentType, "application/msword", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(file.ContentType, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", StringComparison.OrdinalIgnoreCase);
                if (!isValidFileType)
                {
                    Message = "Invalid file extension - uploads word/pdf/excel/txt file only";
                }
                else if (file.Length > (FileSize * 1024))
                {
                    Message = "File size should be upto " + FileSize + "KB";
                }
                else
                {
                    Message = "File Is Successfully Uploaded";
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Message = "Upload Container Should Not Be Empty";
            }
            return result;
        }
    }
}
