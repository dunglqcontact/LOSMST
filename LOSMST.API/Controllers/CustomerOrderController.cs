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
        public IActionResult InsertCusomterOrder(CustomerOrderInsertModel customerOrderInsert)
        {
            var customerOrder = _customerOrderService.InsertCart(customerOrderInsert);
            if (customerOrder != null)
            {
                return Ok(customerOrder);
            }
            return BadRequest();
        }

        [HttpPut("customer-order-cancel")]

        public IActionResult CancelCustomerOrder(CustomerOrder customerOrder)
        {
            if (_customerOrderService.CancelCustomerOrder(customerOrder.Id, customerOrder.Reason))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("customer-order-denied")]

        public IActionResult DenyCustomerOrder(CustomerOrder customerOrder)
        {
            if (_customerOrderService.DenyCustomerOrder(customerOrder.Id, customerOrder.Reason))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("customer-order-approvement")]

        public IActionResult ApproveCustomerOrder([FromBody] CustomerOrder customerOrder)
        {
            if (_customerOrderService.ApproveCustomerOrder(customerOrder.Id, customerOrder.EstimatedReceiveDate, customerOrder.ManagerAccountId))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("customer-order-finish")]

        public IActionResult FinishCustomerOrder([FromQuery] string customerOrderId, [FromQuery] int staffAccountId)
        {
            if (_customerOrderService.FinishCustomerOrder(customerOrderId, staffAccountId))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
