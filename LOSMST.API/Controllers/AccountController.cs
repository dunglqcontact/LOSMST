using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
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

        [HttpGet]
        public IActionResult GetAccounts([FromQuery] AccountParameter accountParam, [FromQuery] PagingParameter paging)
        {
            var data = _accountService.GetAllAccounts(accountParam, paging);
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
    }
}
