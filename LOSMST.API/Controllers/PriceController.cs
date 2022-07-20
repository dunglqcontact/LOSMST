using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Data.OleDb;
using System.Net.Http.Headers;
using LOSMST.Business.Service;

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
        public IActionResult ImportPrice(string fileUrl, string fileName)
        {
            var data = _priceService.ImportPrice(fileUrl, fileName);
            return Ok(data);
        }
    }
}
