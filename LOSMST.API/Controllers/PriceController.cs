using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Data.OleDb;
using System.Net.Http.Headers;
using LOSMST.Business.Service;
using LOSMST.Models.Helper.InsertHelper;

namespace LOSMST.API.Controllers
{
    [Route("api/prices")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly PriceService _priceService;

        public PriceController(PriceService priceDetailService)
        {
            _priceService = priceDetailService;
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
    }
}
