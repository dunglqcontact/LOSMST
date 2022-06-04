using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace LOSMST.API.Controllers
{
    [Route("api/[controller]")]
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
            return Ok(await _authService.Login(loginRequest));
        }
    }
}
