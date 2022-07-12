using LOSMST.Business.Service;
using LOSMST.Models.Database;
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
            var metadata = new
            {
                data,
                data.TotalCount,
                data.PageSize,
                data.CurrentPage,
                data.TotalPages,
                data.HasNext,
                data.HasPrevious
            };
            return Ok(metadata);
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

        [HttpPut("approve")]
        public IActionResult ApproveStoreRequestOrder(StoreRequestOrder storeRequestOrder)
        {
            if (_storeRequestOrderService.ApproveStoreRequestOrder(storeRequestOrder))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("store-request-order-cancel")]

        public IActionResult CancelCustomerOrder(StoreRequestOrder storeRequestOrder)
        {
            if (_storeRequestOrderService.CancelStoreRequestOrder(storeRequestOrder.Id, storeRequestOrder.Reason))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("store-request-order-deny")]

        public IActionResult DenyCustomerOrder(StoreRequestOrder storeRequestOrder)
        {
            if (_storeRequestOrderService.DenyStoreRequestOrder(storeRequestOrder.Id, storeRequestOrder.Reason))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("order-finished")]
        public IActionResult AddImportInventory(string storeRequestId)
        {
            if (_storeRequestOrderService.FinishOrder(storeRequestId))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
