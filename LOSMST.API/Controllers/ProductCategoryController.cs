using LOSMST.Business.Service;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/productCategories")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ProductCategoryService _productCategoryService;

        public ProductCategoryController(ProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public IActionResult GetProductCategories([FromQuery] ProductCategoryParameter productCategoryParam, [FromQuery] PagingParameter paging)
        {
            var data = _productCategoryService.GetAllProductCategories(productCategoryParam, paging);
            return Ok(data);
        }
    }
}
