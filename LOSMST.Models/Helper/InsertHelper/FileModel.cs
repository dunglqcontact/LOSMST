using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.InsertHelper
{
    public class FileModel
    {
        public string? FileName { get; set; }
        public IFormFile FormFile { get; set; }
        //public List<IFormFile> FormFiles { get; set; }
    }
}
