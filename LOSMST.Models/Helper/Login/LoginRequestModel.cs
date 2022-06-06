using System;
using System.Collections.Generic;
using System.Text;

namespace LOSMST.Models.Helper.Login
{
    public class LoginRequestModel
    {
        public string IdToken { get; set; }

        public string? AccessToken { get; set; }

        public string? ProviderId { get; set; }

        public string? SignInMethod { get; set; }
    }
}
