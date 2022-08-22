using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class ExportInventoryDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double? Price { get; set; }
        public string ProductDetailId { get; set; } = null!;
        public string ExportInventoryId { get; set; } = null!;

        public virtual ExportInventory ExportInventory { get; set; } = null!;
        public virtual ProductDetail ProductDetail { get; set; } = null!;
    }
}
