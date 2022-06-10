using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.SearchingModel
{
    public class ProductMinMaxPriceSearchHelper
    {
        public Product product { get; set; }
        public IEnumerable<double>  MinMaxPrice { get; set; }
    }
}
