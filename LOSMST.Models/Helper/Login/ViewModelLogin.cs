using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.Login
{
    public class ViewModelLogin
    {
        public string JwtToken { get; set; }
        public int Id { get; set; }
        public string? Fullname  { get; set; }
        public int? StoreId { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string StatusId { get; set; }
    }
}
