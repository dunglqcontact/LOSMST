using LOSMST.Business.Service;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _statusService;

        public StatusController(StatusService statusService)
        {
            _statusService = statusService;
        }
        [HttpGet]
        public IActionResult GetAllStatus([FromQuery] StatusParameter statusParam, [FromQuery] PagingParameter paging)
        {
            var data = _statusService.GetAllStatus(statusParam, paging);
            return Ok(data);
        }
    }
}
