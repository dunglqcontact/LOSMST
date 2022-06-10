using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace LOSMST.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<ViewModelLogin>> Login(LoginEmailPassword loginRequest)
        {
            var value = await _authService.Login(loginRequest);
            if (value != null)
            {
                if (value.StatusId != "1.1") return BadRequest("account is disable");
                return Ok(value);
            }
            return BadRequest("Email or password is not correct. Please try again!");
        }
        [HttpPost("sign-in-google")]
        public async Task<ActionResult<ViewModelLogin>> LoginGoogle([FromQuery] LoginRequestModel loginRequest)
        {
            var value = await _authService.LoginGoogle(loginRequest);
            if (value != null)
            {
                if (value.StatusId != "1.1") return BadRequest("account is disable");
                return Ok(value);
            }
            return BadRequest("MSG93");
        }
    }
}
