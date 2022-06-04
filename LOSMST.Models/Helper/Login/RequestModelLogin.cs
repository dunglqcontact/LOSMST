using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.Login
{
    public class RequestModelLogin
    {
        public string IdToken { get; set; }

        public string deviceId { get; set; }

        public string AccessToken { get; set; }

        public string ProviderId { get; set; }

        public string SignInMethod { get; set; }
    }
}
