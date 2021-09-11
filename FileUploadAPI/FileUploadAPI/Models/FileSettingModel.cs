using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadAPI.Models
{
    public class FileSettingModel
    {
        public int FileSize { get; set; }
        public string Location { get; set; }
        public FileTypeMap[] FileType { get; set; }
    }
    public class FileTypeMap
    {
        public string Key { get; set; }
        public byte[] Value { get; set; }
    }
}
