using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.DBOHelper
{
    public class ProductDetailParameter
    {
        public string? Id { get; set; } = null;
        public int? ProductId { get; set; }
        public double? Volume { get; set; }
        public string? PackageId { get; set; } = null;
        public string? StatusId { get; set; } = null;
        public int CategoryId { get; set; }
        public int? WholeSalePriceQuantity { get; set; }
        public string? dir { get; set; } = "asc";
        public string? sort { get; set; } = null;
        public string? fields { get; set; } = null;
        public string? includeProperties { get; set; } = null;
    }
}
