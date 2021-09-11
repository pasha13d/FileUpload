﻿using FileUploadAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadAPI
{
    public class FileUploadService
    {
        private byte[] fileByte;
        public FileUploadViewModel UploadUserFile(FileUploadViewModel fileUpload)
        {
            //byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            //FileUploadViewModel fileUpload = new FileUploadViewModel();
            if (fileUpload.File.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileUpload.File.CopyTo(ms);
                    fileByte = ms.ToArray();
                }
            }
            //bool result = false;
            fileUpload.IsSuccess = false;
            try
            {
                var contentType = fileUpload.FileType
                    .Where(m => m.Key.Equals(fileUpload.File.ContentType, StringComparison.OrdinalIgnoreCase)
                    && fileByte.Take(7).ToArray().SequenceEqual(m.Value)
                    )
                    .FirstOrDefault();

                if (contentType == null)
                {
                    fileUpload.Message = "Invalid file extension - uploads word/pdf/excel/txt file only";
                }
                else
                {

                    if (fileUpload.File.Length > (fileUpload.FileSize * 1024))
                    {
                        fileUpload.Message = "File size should be upto " + fileUpload.FileSize + "KB";
                    }
                    else
                    {
                        fileUpload.Message = "File Is Successfully Uploaded";
                        fileUpload.IsSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                fileUpload.Message = "Upload Container Should Not Be Empty";
            }
            return fileUpload;
        }
    }
}
