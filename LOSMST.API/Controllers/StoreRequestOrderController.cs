using LOSMST.Business.Service;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.InsertHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/store-request-order")]
    [ApiController]
    public class StoreRequestOrderController : ControllerBase
    {
        private readonly StoreRequestOrderService _storeRequestOrderService;

        public StoreRequestOrderController(StoreRequestOrderService storeRequestOrderService)
        {
            _storeRequestOrderService = storeRequestOrderService;
        }

        [HttpGet]
        public IActionResult GetStoreCategories([FromQuery] StoreRequestOrderParameter storeRequestOrderParam, [FromQuery] PagingParameter paging)
        {
            var data = _storeRequestOrderService.GetAllStoreRequestOrder(storeRequestOrderParam, paging);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult InsertStoreRequestOrder(StoreRequestOrderInsertModel storeRequestOrder)
        {
            if (_storeRequestOrderService.InsertStoreRequestOrder(storeRequestOrder))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
