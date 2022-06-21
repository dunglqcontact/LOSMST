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
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

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

        public async Task<ViewModelLogin> VerifyFirebaseTokenIdRegister(string idToken)
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("capstone-project-edc2a-firebase-adminsdk-w5qk4-37e986decb.json"),
                });
            }

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
            var account = _accountRepository.GetFirstOrDefault(x => x.Email == user.Email);
            if (account == null)
            {
                Account userInfo = new Account();
                userInfo.Email = user.Email;
                //  userInfo.Fullname = user.DisplayName;
                userInfo.RoleId = "U06";

                try
                {
                    _accountRepository.Add(userInfo);
                    _accountRepository.SaveDbChange();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
            account = _accountRepository.GetFirstOrDefault(x => x.Email == user.Email);
            if (account != null)
            {
                var loginViewModel = new ViewModelLogin
                {
                    Id = account.Id,
                    Email = account.Email,
                    RoleId = account.RoleId,
                    StatusId = account.StatusId,
                    Phone = account.Phone,
                    Avatar  = account.Avatar,
                    Fullname = account.Fullname,
                    JwtToken = null,
                };
                var values = loginViewModel;
                return loginViewModel;
            }
            return null;
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
                    Fullname = checkAccount.Fullname,
                    StoreId = checkAccount.StoreId,
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
                new Claim(ClaimTypes.Role, user.RoleId),
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim(ClaimTypes.Email, user.Email)
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
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenHandler = new JwtSecurityTokenHandler();
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



        public async Task<ViewModelLogin> LoginGoogle(LoginRequestModel loginRequest)
        {
            var userViewModel = await VerifyFirebaseTokenIdRegister(loginRequest.IdToken);
            if (userViewModel != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userViewModel.RoleId),
                //new Claim(ClaimTypes.Name, userViewModel.Fullname),
                new Claim(ClaimTypes.Email, userViewModel.Email)
            };

                var accessToken = GenerateAccessToken(claims);
                // var refreshToken = GenerateRefreshToken();

                userViewModel.JwtToken = accessToken;
            }
            return userViewModel;
        }
    }
}