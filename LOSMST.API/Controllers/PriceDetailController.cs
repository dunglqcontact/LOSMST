using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
using Microsoft.AspNetCore.Http;
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
