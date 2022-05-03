using LOSMST.Business.Service;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/packages")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly PackageService _packageService;

        public PackageController(PackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet]
        public IActionResult GetPackages([FromQuery] PackageParameter packageParam, [FromQuery] PagingParameter paging)
        {
            var data = _packageService.GetAllPackages(packageParam, paging);
            return Ok(data);
        }
    }
}
