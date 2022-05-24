using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.DBOHelper
{
    public class StoreProductDetailParameter
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string? ProductDetailId { get; set; } = null!;
        public int? CurrentQuantity { get; set; }
        public int? CurrentVolume { get; set; }

        public string? ProductDetail { get; set; } = null!;
        public string? Store { get; set; } = null!;
        public string? dir { get; set; } = "asc";
        public string? sort { get; set; } = null;
        public string? fields { get; set; } = null;
        public string? includeProperties { get; set; } = null;
    }
}
