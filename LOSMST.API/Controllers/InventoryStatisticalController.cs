using LOSMST.Business.Service;
using LOSMST.Models.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/inventory-statistical")]
    [ApiController]
    public class InventoryStatisticalController : ControllerBase
    {
        private readonly InventoryStatisticalService _inventoryStatisticalService;

        public InventoryStatisticalController(InventoryStatisticalService inventoryStatisticalService)
        {
            _inventoryStatisticalService = inventoryStatisticalService;
        }

        [HttpGet]
        public IActionResult GetResult(DateTime fromDate, DateTime toDate, int storeId)
        {
            var data = _inventoryStatisticalService.GetAllInventoryStatistical(fromDate, toDate, storeId);
            return Ok(data);
        }
    }
}
