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

        [HttpGet]
        public IActionResult GetImportInventory([FromQuery] ImportInventoryParameter importInventoryParam, [FromQuery] PagingParameter paging)
        {
            var data = _importInventoryService.GetAllImportInventory(importInventoryParam, paging);
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
