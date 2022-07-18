using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class ImportInventory
    {
        public ImportInventory()
        {
            ImportInventoryDetails = new HashSet<ImportInventoryDetail>();
        }

        public string? Id { get; set; } = null!;
        public DateTime? ImportDate { get; set; }
        public int StoreId { get; set; } = 0;

        public virtual Store? Store { get; set; } = null!;
        public virtual ICollection<ImportInventoryDetail> ImportInventoryDetails { get; set; }
    }
}
