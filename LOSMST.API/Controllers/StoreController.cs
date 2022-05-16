using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _storeService;

        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public IActionResult GetStore([FromQuery] StoreParameter storeParam, [FromQuery] PagingParameter paging)
        {
            var data = _storeService.GetAllStores(storeParam, paging);
            return Ok(data);
        }

        [HttpGet("manager-existence")]
        public IActionResult CheckStoreManager(string storeCode, string roleId)
        {
            var data = _storeService.CheckStoreManager(storeCode, roleId);
            return Ok(data);
        }

        [HttpGet("current-store-code")]
        public IActionResult GetCurrentStoreByStoreCode(string storeCode)
        {
            var data = _storeService.GetCurrentStoreByStoreCode(storeCode);
            return Ok(data);
        }

        [HttpGet("existed-store-email")]
        public IActionResult CheckCurrentStoreByStoreEmail(string storeEmail)
        {
            var data = _storeService.CheckCurrentStoreByStoreEmail(storeEmail);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddStore(Store store)
        {
            if (_storeService.Add(store))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateStore(Store store)
        {

            if (_storeService.Update(store))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
