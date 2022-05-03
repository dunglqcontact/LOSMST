using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class Package
    {
        public Package()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
