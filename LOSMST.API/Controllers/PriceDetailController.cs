using LOSMST.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/priceDetails")]
    [ApiController]
    public class PriceDetailController : ControllerBase
    {
        private readonly PriceDetailService _priceDetailService;

        public PriceDetailController(PriceDetailService priceDetailService)
        {
            _priceDetailService = priceDetailService;
        }
    }
}
