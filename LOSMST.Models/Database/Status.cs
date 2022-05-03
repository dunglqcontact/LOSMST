using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class Status
    {
        public Status()
        {
            Accounts = new HashSet<Account>();
            CustomerOrders = new HashSet<CustomerOrder>();
            Prices = new HashSet<Price>();
            ProductDetails = new HashSet<ProductDetail>();
            Products = new HashSet<Product>();
            StoreRequestOrders = new HashSet<StoreRequestOrder>();
            Stores = new HashSet<Store>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<StoreRequestOrder> StoreRequestOrders { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
