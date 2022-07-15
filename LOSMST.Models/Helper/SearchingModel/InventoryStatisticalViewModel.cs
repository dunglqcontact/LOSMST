using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.SearchingModel
{
    public class InventoryStatisticalViewModel
    {
        public string? ProductDetailId { get; set; }
        public string? ProductName { get; set; }
        public int? BeginingNumberPeriod { get; set; } = 0;
        public int? ImportInPeriod { get; set; } = 0;
        public int? ExportInPeriod { get; set; } = 0;
    }
}
