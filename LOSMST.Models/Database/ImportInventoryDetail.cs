using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class ImportInventoryDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string ProductDetailId { get; set; } = null!;
        public string? ImportInventoryId { get; set; } = null!;

        public virtual ImportInventory? ImportInventory { get; set; } = null!;
        public virtual ProductDetail? ProductDetail { get; set; } = null!;
    }
}
