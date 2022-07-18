using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/export-inventory")]
    [ApiController]
    public class ExportInventoryController : ControllerBase
    {
        private readonly ExportInventoryService _exportInventoryService;

        public ExportInventoryController(ExportInventoryService exportInventory)
        {
            _exportInventoryService = exportInventory;
        }

        [HttpGet]
        public IActionResult GetExportInventory([FromQuery] ExportInventoryParameter exportInventoryParam, [FromQuery] PagingParameter paging)
        {
            var data = _exportInventoryService.GetAllAccounts(exportInventoryParam, paging);
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
