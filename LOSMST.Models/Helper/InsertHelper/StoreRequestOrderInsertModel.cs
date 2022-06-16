using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.InsertHelper
{
    public class StoreRequestOrderInsertModel
    {
        public int StoreRequestId { get; set; }
        public string StoreSupplyCode { get; set; }
        public List<ProductStoreRequestOrder> productStoreRequestOrders { get; set; }
    }
    public class ProductStoreRequestOrder
    {
        public string ProductDetailId { get; set; }
        public string StoreRequestOrderId { get; set; }
        public int Quantity { get; set; }
    }
}
