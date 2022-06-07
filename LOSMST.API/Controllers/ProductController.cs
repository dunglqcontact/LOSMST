using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery] ProductParameter productParam, [FromQuery] PagingParameter paging)
        {
            var data = _productService.GetAllProducts(productParam, paging);
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
        /*[HttpGet("product-price")]
        public IActionResult GetProductsWithMinMaxPrice([FromQuery] ProductParameter productParam, [FromQuery] PagingParameter paging)
        {
            List<ProductMinMaxPriceSearchHelper> metaValue = new List<ProductMinMaxPriceSearchHelper>();
            var data = _productService.GetAllProducts(productParam, paging);
            foreach (var item in data)
            {
                var price = _priceDetailService.GetMinMaxPrice(item.Id);
                metaValue.Add(new ProductMinMaxPriceSearchHelper
                {
                    product = item,
                    MinMaxPrice = price,
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

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (_productService.Add(product))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {

            if (_productService.Update(product))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("product-disable")]
        public IActionResult DisableProduct(int productId)
        {

            if (_productService.DisableProduct(productId))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
