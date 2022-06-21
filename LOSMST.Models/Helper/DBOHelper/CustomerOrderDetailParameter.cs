using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.DBOHelper
{
    public class CustomerOrderDetailParameter
    {
        public int? Id { get; set; }
        public string? CustomerOrderId { get; set; } = null!;
        public string? ProductDetailId { get; set; } = null!;
        public int? Quantity { get; set; }
        public double? Price { get; set; }

        public string? CustomerOrder { get; set; } = null!;
        public string? ProductDetail { get; set; } = null!;

        public string? dir { get; set; } = "asc";
        public string? sort { get; set; } = null;
        public string? fields { get; set; } = null;
        public string? includeProperties { get; set; } = null;
    }
}
