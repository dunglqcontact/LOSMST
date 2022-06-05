using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Helper.Login;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using LOSMST.Models.Database;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace LOSMST.Business.Service
{
    public class AuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _accountRepository = accountRepository;
        }

        //verify for register
        public async Task<ViewModelLogin> VerifyFirebaseTokenIdRegister(string idToken, string roleId, string fullname)
        {
            FirebaseToken decodedToken;
            try
            {
                decodedToken = await FirebaseAuth.DefaultInstance
                       .VerifyIdTokenAsync(idToken);
            }
            catch
            {
                throw new Exception();
            }
            string uid = decodedToken.Uid;
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

            Account account = new Account();
            account.Email = user.Email;
            account.Fullname = fullname;
            account.RoleId = roleId;

            try
            {
                _accountRepository.Add(account);
                _accountRepository.SaveDbChange();
            }
            catch (Exception)
            {
                throw new Exception();
            }

            // Query account table in DB
            var checkAccount = _accountRepository.GetFirstOrDefault(x => x.Email == user.Email);

            if (checkAccount == null) throw new UnauthorizedAccessException();

            var viewLoginModel = new ViewModelLogin
            {
                Id = checkAccount.Id,
                Email = checkAccount.Email,
                JwtToken = null
            };
            return viewLoginModel;
        }

        public async Task<ViewModelLogin?> VerifyAccount(LoginEmailPassword loginRequest)
        {
            // Query account table in DB
            var checkAccount = _accountRepository.GetFirstOrDefault(x => x.Email == loginRequest.Email && x.Password == loginRequest.Password);
            if (checkAccount != null)
            {
                var viewLoginModel = new ViewModelLogin
                    {
                        Id = checkAccount.Id,
                        Email = checkAccount.Email,
                        RoleId = checkAccount.RoleId,
                        StatusId = checkAccount.StatusId,
                        JwtToken = null
                    };
                return viewLoginModel;
            }
            return null;
        }

        //create token
        private string CreateToken(ViewModelLogin user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.RoleId)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        public async Task<ViewModelLogin> Login(LoginEmailPassword loginRequest)
        {
            var valueBytes = Encoding.UTF8.GetBytes(loginRequest.Password);
            loginRequest.Password = Convert.ToBase64String(valueBytes);
            var userViewModel = await VerifyAccount(loginRequest);
            if (userViewModel != null)
            {
                var accessToken = CreateToken(userViewModel);
                // var refreshToken = GenerateRefreshToken();

                userViewModel.JwtToken = accessToken;
                return userViewModel;
            }
            return null;
        }
    }
}
