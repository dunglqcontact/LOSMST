using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.InsertHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/customer-order-details")]
    [ApiController]
    public class CustomerOrderDetailController : ControllerBase
    {
        private readonly CustomerOrderDetailService _customerOrderDetailService;

        public CustomerOrderDetailController(CustomerOrderDetailService customerOrderDetailService)
        {
            _customerOrderDetailService = customerOrderDetailService;
        }

        [HttpGet]
        public IActionResult GetCustomerOrderDetails([FromQuery] CustomerOrderDetailParameter customerParam, [FromQuery] PagingParameter paging)
        {
            var data = _customerOrderDetailService.GetAllCustomerOrderDetail(customerParam, paging);
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

        [HttpGet("inventory")]
        public IActionResult GetCustomerOrderDetailsWithInventory([FromQuery] string customerOrderId)
        {
            var data = _customerOrderDetailService.GetAllCustomerOrderDetailWithInventory(customerOrderId);

            return Ok(data);
        }
    }
}
