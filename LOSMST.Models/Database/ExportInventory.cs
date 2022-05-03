using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class ExportInventory
    {
        public ExportInventory()
        {
            ExportInventoryDetails = new HashSet<ExportInventoryDetail>();
        }

        public string Id { get; set; } = null!;
        public DateTime ExportDate { get; set; }
        public int StoreId { get; set; }

        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<ExportInventoryDetail> ExportInventoryDetails { get; set; }
    }
}
