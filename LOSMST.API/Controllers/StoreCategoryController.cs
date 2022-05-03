using LOSMST.Business.Service;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/storeCategories")]
    [ApiController]
    public class StoreCategoryController : ControllerBase
    {
        private readonly StoreCategoryService _storeCategoryService;

        public StoreCategoryController(StoreCategoryService storeCategoryService)
        {
            _storeCategoryService = storeCategoryService;
        }

        [HttpGet]
        public IActionResult GetStoreCategories([FromQuery] StoreCategoryParameter storeCategoryParam, [FromQuery] PagingParameter paging)
        {
            var data = _storeCategoryService.GetAllStoreCategories(storeCategoryParam, paging);
            return Ok(data);
        }
    }
}
