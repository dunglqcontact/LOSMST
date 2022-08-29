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
using System.Drawing;
using OfficeOpenXml.Style;
using System.Text.RegularExpressions;

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
        public async Task<IActionResult> ImportPrice([FromForm] FileModel file)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.FormFile.CopyTo(stream);
            }
            var data = _priceService.ImportPrice(path, file.FileName);
            if (data == false)
            {
                return BadRequest();
            }
            return Ok(true);
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


        [HttpPost("exportv2")]
        public async Task<IActionResult> ExportV2([FromForm] FileModel file, CancellationToken cancellationToken)
        {
            var errorRowList = new List<int>();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);
            FileInfo fileInfo = new FileInfo(path);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage packageSample = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheetSample = packageSample.Workbook.Worksheets.FirstOrDefault();

            int colCount = worksheetSample.Dimension.End.Column;
            int rowCount = worksheetSample.Dimension.End.Row;
            for (int row = 2; row <= rowCount; row++)
            {
                PriceDetail priceDetail = new PriceDetail();
                for (int col = 1; col <= colCount; col++)
                {
                    if (col == 1)
                    {
                        var value = worksheetSample.Cells[row, col].Value;
                        if (value == null)
                        {
                            var checkErrorExisted = errorRowList.FirstOrDefault(x => x == row);
                            if (checkErrorExisted == 0)
                            {
                                errorRowList.Add(row);
                            }
                            break;
                        }
                        else
                        {
                            if(_priceService.GetProductDetails().FirstOrDefault(x => x.Id == value.ToString()) == null)
                            {
                                var checkErrorExisted = errorRowList.FirstOrDefault(x => x == row);
                                if (checkErrorExisted == 0)
                                {
                                    errorRowList.Add(row);
                                }
                                break;
                            }
                        }

                    }
                    if (col == 6)
                    {
                        var value = worksheetSample.Cells[row, col].Value;

                        if (value == null)
                        {
                            var checkErrorExisted = errorRowList.FirstOrDefault(x => x == row);
                            if (checkErrorExisted == 0)
                            {
                                errorRowList.Add(row);
                            }
                            break;
                        }
                        else
                        {
                            bool checkValid = Regex.IsMatch(value.ToString(), "([a-zA-Z!@#$%^&*()_=+<>/?`~-])");

                            if (checkValid)
                            {
                                var checkErrorExisted = errorRowList.FirstOrDefault(x => x == row);
                                if (checkErrorExisted == 0)
                                {
                                    errorRowList.Add(row);
                                }
                                break;
                            }
                        }
                    }
                    if (col == 7)
                    {
                        var value = worksheetSample.Cells[row, col].Value;

                        if (value == null)
                        {
                            var checkErrorExisted = errorRowList.FirstOrDefault(x => x == row);
                            if (checkErrorExisted == 0)
                            {
                                errorRowList.Add(row);
                            }
                            break;
                        }
                        else
                        {
                            bool checkValid = Regex.IsMatch(value.ToString(), "([a-zA-Z!@#$%^&*()_=+<>/?`~-])");

                            if (checkValid)
                            {
                                var checkErrorExisted = errorRowList.FirstOrDefault(x => x == row);
                                if (checkErrorExisted == 0)
                                {
                                    errorRowList.Add(row);
                                }
                                break;
                            }
                        }
                    }
                }
            }


            await Task.Yield();
            var list = new List<PriceExportModel>();

            var stream = new MemoryStream();



            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Bảng giá dầu mỡ nhờn", worksheetSample);
                foreach (var rowItem in errorRowList)
                {
                    using (var range = workSheet.Cells[rowItem, 1, rowItem, 7])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Red);
                    }
                }
                package.Save();
            }
            stream.Position = 0;
            //string excelName = $"Bang gia dau mo nhon {DateTime.Now.ToString("dd_MM_yyyy HHHH_mm_ss")} Error.xlsx";
            string excelName = $"conchonocanconmeo.xlsx";
            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

    }
}
