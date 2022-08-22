using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
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

        [HttpGet("product-detail-price")]
        public async Task<IActionResult> GetProductDetailWithPrice([FromQuery] ProductDetailParameter productDetailParam, [FromQuery] PagingParameter paging)
        {
            var data = await _productDetailService.GetProductDetailWithPrice(productDetailParam, paging);
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

        [HttpGet("all-product-detail-price")]
        public IActionResult GetAllProductDetailWithPrice([FromQuery] ProductDetailParameter productDetailParam, [FromQuery] PagingParameter paging, bool isPaging = true)
        {
            if (isPaging == true)
            {
                var data = _productDetailService.GetAllProductDetailWithPrice(productDetailParam, paging);
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
            else
            {
                var data = _productDetailService.GetAllProductDetailWithPriceNonPaging(productDetailParam, paging);
                return Ok(data);
            }
        }

        [HttpPost("cart")]
        public IActionResult GetProductInCart([FromBody] ListIdString listIdString,[FromQuery] ProductDetailParameter productDetailParam, [FromQuery] PagingParameter paging)
        {
            var data = _productDetailService.GetProductDetailByListId(listIdString, productDetailParam, paging);
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

        [HttpPost("store-cart")]
        public IActionResult GetProductInStoreCart([FromBody] ListIdString listIdString, [FromQuery] ProductDetailParameter productDetailParam, [FromQuery] PagingParameter paging)
        {
            var data = _productDetailService.GetStoreCart(listIdString, productDetailParam, paging);
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
