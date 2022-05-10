using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class Account
    {
        public Account()
        {
            CustomerOrders = new HashSet<CustomerOrder>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? Avartar { get; set; }
        public string RoleId { get; set; } = null!;
        public int? StoreId { get; set; }
        public bool Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? StatusId { get; set; } = null!;

        public virtual Role? Role { get; set; } = null!;
        public virtual Status? Status { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}
