using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class StoreCategory
    {
        public StoreCategory()
        {
            Stores = new HashSet<Store>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Store> Stores { get; set; }
    }
}
