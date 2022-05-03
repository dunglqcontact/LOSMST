using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class Product
    {
        public Product()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int CategoryId { get; set; }
        public string QualityLevelFeature { get; set; } = null!;
        public string Brief { get; set; } = null!;
        public string GeneralBenefit { get; set; } = null!;
        public string Apply { get; set; } = null!;
        public string Preserve { get; set; } = null!;
        public string? StatusId { get; set; } = null!;

        public virtual ProductCategory? Category { get; set; } = null!;
        public virtual Status? Status { get; set; } = null!;
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
