using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class StoreProductDetail
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string ProductDetailId { get; set; } = null!;
        public int CurrentQuantity { get; set; }

        public virtual ProductDetail ProductDetail { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
    }
}
