using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class PriceDetail
    {
        public int Id { get; set; }
        public string ProductDetailId { get; set; } = null!;
        public double ImportPrice { get; set; }
        public double RetailPriceBeforeTax { get; set; }
        public double WholesalePriceBeforeTax { get; set; }
        public double RetailPriceAfterTax { get; set; }
        public double WholesalePriceAfterTax { get; set; }
        public string PriceId { get; set; } = null!;

        public virtual Price Price { get; set; } = null!;
        public virtual ProductDetail ProductDetail { get; set; } = null!;
    }
}
