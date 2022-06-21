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

        [HttpGet]
        public IActionResult GetCustomerOrders([FromQuery] CustomerOrderParameter customerParam, [FromQuery] PagingParameter paging)
        {
            var data = _customerOrderService.GetAllCustomerOrder(customerParam, paging);
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

        [HttpPost]
        public IActionResult InsertCusomterOrder(CustomerOrderInsertModel customerOrder)
        {
            if (_customerOrderService.InsertCart(customerOrder))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("customer-order-cancel")]

        public IActionResult CancelCustomerOrder([FromQuery] string id)
        {
            if (_customerOrderService.CancelCustomerOrder(id))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
