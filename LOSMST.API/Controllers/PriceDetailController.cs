using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/priceDetails")]
    [ApiController]
    public class PriceDetailController : ControllerBase
    {
        private readonly PriceDetailService _priceDetailService;

        public PriceDetailController(PriceDetailService priceDetailService)
        {
            _priceDetailService = priceDetailService;
        }

        [HttpGet]
        public IActionResult GetPriceDetails([FromQuery] ProductParameter productParam, [FromQuery] PagingParameter paging)
        {
            var data = _priceDetailService.GetAllProducts(productParam, paging);
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
        /*[HttpGet("priceDetail-priceDetail")]
        public IActionResult GetPriceDetailsWithMinMaxPriceDetail([FromQuery] PriceDetailParameter priceDetailParam, [FromQuery] PagingParameter paging)
        {
            List<PriceDetailMinMaxPriceDetailSearchHelper> metaValue = new List<PriceDetailMinMaxPriceDetailSearchHelper>();
            var data = _priceDetailService.GetAllPriceDetails(priceDetailParam, paging);
            foreach (var item in data)
            {
                var priceDetail = _priceDetailDetailService.GetMinMaxPriceDetail(item.Id);
                metaValue.Add(new PriceDetailMinMaxPriceDetailSearchHelper
                {
                    priceDetail = item,
                    MinMaxPriceDetail = priceDetail,
                });
            }
            var metadata = new
            {
                metaValue,
                data.TotalCount,
                data.PageSize,
                data.CurrentPage,
                data.TotalPages,
                data.HasNext,
                data.HasPrevious
            };
            return Ok(metadata);
        }*/

      
    }
}
