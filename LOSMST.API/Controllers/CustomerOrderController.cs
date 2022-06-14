using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.InsertHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/customer-orders")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly CustomerOrderService _customerOrderService;

        public CustomerOrderController(CustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [HttpPost]
        public IActionResult InsertCusomterOrder(CustomerOrderInsertModel customerOrder)
        {
            if (_customerOrderService.InsertCart(customerOrder))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
