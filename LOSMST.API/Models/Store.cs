using System;
using System.Collections.Generic;

namespace LOSMST.API.Models
{
    public partial class Store
    {
        public Store()
        {
            Accounts = new HashSet<Account>();
            CustomerOrders = new HashSet<CustomerOrder>();
            ExportInventories = new HashSet<ExportInventory>();
            ImportInventories = new HashSet<ImportInventory>();
            StoreProductDetails = new HashSet<StoreProductDetail>();
            StoreRequestOrders = new HashSet<StoreRequestOrder>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string StoreCategoryId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Ward { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string StatusId { get; set; } = null!;

        public virtual Status Status { get; set; } = null!;
        public virtual StoreCategory StoreCategory { get; set; } = null!;
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
        public virtual ICollection<ExportInventory> ExportInventories { get; set; }
        public virtual ICollection<ImportInventory> ImportInventories { get; set; }
        public virtual ICollection<StoreProductDetail> StoreProductDetails { get; set; }
        public virtual ICollection<StoreRequestOrder> StoreRequestOrders { get; set; }
    }
}
