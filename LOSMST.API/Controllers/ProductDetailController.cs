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
            return Ok(data);
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
        public IActionResult DeleteProductDetail(string productDetailId)
        {

            if (_productDetailService.Delete(productDetailId))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
