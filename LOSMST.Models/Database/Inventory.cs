using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string ProductDetailId { get; set; } = null!;
        public int CurrentQuantity { get; set; }
        public int? CurrentVolume { get; set; }

        public virtual Store Store { get; set; } = null!;
    }
}
