using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class ProductStoreRequestDetail
    {
        public string Id { get; set; } = null!;
        public string ProductDetailId { get; set; } = null!;
        public string StoreRequestOrderId { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual ProductDetail ProductDetail { get; set; } = null!;
        public virtual StoreRequestOrder StoreRequestOrder { get; set; } = null!;
    }
}
