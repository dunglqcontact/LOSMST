using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.InsertHelper
{
    public class CustomerOrderInsertModel
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public double TotalPrice { get; set; }
        public List<CustomerOrderDetailInsert> customerOrderDetails { get; set; }
    }

    public class CustomerOrderDetailInsert
    {
        public string ProductDetailId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
