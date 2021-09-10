using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadAPI.Models
{
    public class FileUploadViewModel
    {
        public string Message { get; set; }
        public decimal FileSize { get; set; }

        private byte[] fileByte;
        private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
        private static readonly byte[] DOC = { 208, 207, 17, 224, 161, 177, 26, 225 };
        private static readonly byte[] TEXT = { 71, 70, 71, 10 };
        public bool UploadUserFile([FromForm] IFormFile file)
        {
            bool result = false;
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileByte = ms.ToArray();
                }
            }
            try
            {
                bool isValidFileType = (string.Equals(file.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase) && fileByte.Take(7).SequenceEqual(PDF)) ||
                                    string.Equals(file.ContentType, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(file.ContentType, "application/vnd.ms-excel", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(file.ContentType, "text/plain", StringComparison.OrdinalIgnoreCase) ||
                                    (string.Equals(file.ContentType, "application/msword", StringComparison.OrdinalIgnoreCase) && fileByte.Take(8).SequenceEqual(DOC)) ||
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
