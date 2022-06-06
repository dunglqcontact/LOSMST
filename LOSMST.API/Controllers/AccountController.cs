using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.UpdatePassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet, Authorize(Roles = "U01,U06")]
        public IActionResult GetAccounts([FromQuery] AccountParameter accountParam, [FromQuery] PagingParameter paging)
        {
            var data = _accountService.GetAllAccounts(accountParam, paging);
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

        [HttpGet("existed-email")]
        public IActionResult CheckEmailExsitence(string emailStr)
        {
            var data = _accountService.CheckEmaiExisted(emailStr);
            return Ok(data);
        }

        [HttpGet("store-manager")]
        public IActionResult GetStoreManager(string storeCode)
        {
            var data = _accountService.GetStoreManager(storeCode);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddAccount(Account account)
        {
            if (_accountService.Add(account))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateAccount(Account account)
        {

            if (_accountService.Update(account))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("password-change")]
        public IActionResult UpdatePassword(UpdatePassword request)
        {

            if (_accountService.UpdatePassword(request.accountId, request.password))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
