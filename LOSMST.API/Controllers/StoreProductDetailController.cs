using LOSMST.Business.Service;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/store-product-details")]
    [ApiController]
    public class StoreProductDetailController : ControllerBase
    {
        private readonly StoreProductDetailService _storeProductDetailService;

        public StoreProductDetailController(StoreProductDetailService storeProductDetailService)
        {
            _storeProductDetailService = storeProductDetailService;
        }
        [HttpGet]
        public IActionResult GetAllStoreProductDetail([FromQuery] StoreProductDetailParameter storeProductDetailParam, [FromQuery] PagingParameter paging)
        {
            var data = _storeProductDetailService.GetAllStoreProductDetail(storeProductDetailParam, paging);
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
        [HttpGet("specific-store-inventory")]
        public IActionResult GetStoreInventory(int storeId, [FromQuery] PagingParameter paging)
        {
            var data = _storeProductDetailService.GetStoreInventory(storeId, paging);
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
    }
}
