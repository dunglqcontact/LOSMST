using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Data.OleDb;
using System.Net.Http.Headers;
using LOSMST.Business.Service;
using LOSMST.Models.Helper.InsertHelper;
using LOSMST.Models.Database;
using OfficeOpenXml;
using LOSMST.Models.Helper.Utils;

namespace LOSMST.API.Controllers
{
    [Route("api/prices")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly PriceService _priceService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public PriceController(PriceService priceDetailService, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _priceService = priceDetailService;
            this._hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult ImportPrice([FromForm] FileModel file)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.FormFile.CopyTo(stream);
            }
            var data = _priceService.ImportPrice(path, file.FileName);
            return Ok(data);
        }

        [HttpGet("export")]
        public async Task<DemoResponse<string>> Export(CancellationToken cancellationToken)
        {
            string folder = _hostingEnvironment.WebRootPath;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            string downloadUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, excelName);
            FileInfo file = new FileInfo(Path.Combine(folder, excelName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(folder, excelName));
            }

            // query data from database  
            await Task.Yield();

            var list = new List<Role>();
            Role role = new Role
            {
                Id = "abbbc",
                Name = "abc",
            };
            list.Add(role);

            using (var package = new ExcelPackage(file))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }

            return DemoResponse<string>.GetResult(0, "OK", downloadUrl);
        }


        [HttpGet("exportv2")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();
            var list = new List<Role>()
        {
            new Role { Id = "catcher", Name = "1sdfsdf" },
        };
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

    }
}
