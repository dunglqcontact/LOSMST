using System;
using System.Collections.Generic;

namespace LOSMST.API.Models
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
