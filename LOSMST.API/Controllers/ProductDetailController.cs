using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/product-details")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly ProductDetailService _productDetailService;

        public ProductDetailController(ProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        [HttpGet]
        public IActionResult GetProductDetail([FromQuery] ProductDetailParameter productDetailParam, [FromQuery] PagingParameter paging)
        {
            var data = _productDetailService.GetAllProductDetails(productDetailParam, paging);
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
        public IActionResult AddProductDetail(ProductDetail productDetail)
        {
            if (_productDetailService.Add(productDetail))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateProductDetail(ProductDetail productDetail)
        {

            if (_productDetailService.Update(productDetail))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteProductDetail(string id)
        {

            if (_productDetailService.Delete(id))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
