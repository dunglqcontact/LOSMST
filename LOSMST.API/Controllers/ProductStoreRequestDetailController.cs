using LOSMST.Business.Service;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/product-store-request-detail")]
    [ApiController]
    public class ProductStoreRequestDetailController : ControllerBase
    {
        private readonly ProductStoreRequestDetailService _productStoreRequestDetailService;

        public ProductStoreRequestDetailController(ProductStoreRequestDetailService productStoreRequestDetailService)
        {
            _productStoreRequestDetailService = productStoreRequestDetailService;
        }
        [HttpGet("store-supply-inventory")]
        public IActionResult GetAllProductStoreRequestDetailController([FromQuery] string storeRequestOrderId)
        {
            var data = _productStoreRequestDetailService.GetProductStoreRequestDetailInventoryViewModels(storeRequestOrderId);

            return Ok(data);
        }
    }
}
