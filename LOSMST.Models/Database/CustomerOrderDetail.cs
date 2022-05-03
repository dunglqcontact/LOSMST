using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class CustomerOrderDetail
    {
        public int Id { get; set; }
        public string CustomerOrderId { get; set; } = null!;
        public string ProductDetailId { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; } = null!;
        public virtual ProductDetail ProductDetail { get; set; } = null!;
    }
}
