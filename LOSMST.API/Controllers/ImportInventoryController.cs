using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/import-inventory")]
    [ApiController]
    public class ImportInventoryController : ControllerBase
    {
        private readonly ImportInventoryService _importInventoryService;

        public ImportInventoryController(ImportInventoryService importInventory)
        {
            _importInventoryService = importInventory;
        }      

        [HttpPost]
        public IActionResult AddImportInventory(string storeRequestId,[FromBody] ImportInventory importInventory)
        {
            if(_importInventoryService.ImportInventory(storeRequestId, importInventory))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
