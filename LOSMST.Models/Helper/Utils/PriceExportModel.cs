using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.Utils
{
    public class PriceExportModel
    {
        public string ProductDetailId { get; set; }
        public string ProductName { get; set; }
        public string PackageName { get; set; }
        public double Volume { get; set; }
        public string ProductCategory { get; set; }
        public double RetailPrice { get; set; }
        public double WholeSalePrice { get; set; }
    }
}
