using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.DBOHelper
{
    public class AccountParameter
    {
        public int? Id { get; set; }
        public string? notIncludeRoleId { get; set; } = null;
        public string? Email { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public string? Fullname { get; set; } = null!;
        public string? Avartar { get; set; }
        public string? RoleId { get; set; } = null!;
        public int? StoreId { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? StatusId { get; set; } = null!;
        public string? dir { get; set; } = "asc";
        public string? sort { get; set; } = null;
        public string? fields { get; set; } = null;
        public string? includeProperties { get; set; } = null;
    }
}
