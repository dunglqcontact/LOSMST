using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.SearchingModel
{
    public class CustomerOrderDetailInventoryViewModel
    {
        public CustomerOrderDetail CustomerOrderDetail { get; set; }
        public int CurrentQuantity { get; set; }
    }
}
