using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class PriceDetail
    {
        public int Id { get; set; }
        public string ProductDetailId { get; set; } = null!;
        public double ImportPrice { get; set; }
        public double RetailPrice { get; set; }
        public double WholesalePrice { get; set; }
        public string PriceId { get; set; } = null!;

        public virtual Price Price { get; set; } = null!;
        public virtual ProductDetail ProductDetail { get; set; } = null!;
    }
}
