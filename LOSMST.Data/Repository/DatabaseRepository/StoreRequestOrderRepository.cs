using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper.InsertHelper;
using LOSMST.Models.Helper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class StoreRequestOrderRepository : GeneralRepository<StoreRequestOrder>, IStoreRequestOrderRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public StoreRequestOrderRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateRequest(StoreRequestOrderInsertModel storeRequestOrderInsert)
        {
            DateTime orderDateTime = DateTime.Now;
            var dateString = orderDateTime.ToString("yyMMdd");

            string storeRequestId = "00.##";
            string countOrderEachDate = "00.##";

            string storeRequestOrderId = dateString + "" + storeRequestOrderInsert.StoreRequestId.ToString(storeRequestId) 
                                        + storeRequestOrderInsert.StoreSupplyCode;

            storeRequestOrderId = storeRequestOrderId.ToUpper();

            var checkStoreRequestOrderIdTemp = _dbContext.CustomerOrders.Where(x => x.Id.Contains(storeRequestOrderId));
            if (!IEnumerableCheckNull.IsAny(checkStoreRequestOrderIdTemp))
            {
                int count = 1;
                storeRequestOrderId = storeRequestOrderId + count.ToString(countOrderEachDate);
            }
            else
            {
                var lastStoreRequestOrder = checkStoreRequestOrderIdTemp.OrderBy(x => x.Id).Last();
                var id = lastStoreRequestOrder.Id;
                var lastOrderCount = id.Substring(10);
                var count = Int32.Parse(lastOrderCount) + 1;
                storeRequestOrderId = storeRequestOrderId + count.ToString(countOrderEachDate);
            }
            List<ProductStoreRequestDetail> cartList = new List<ProductStoreRequestDetail>();
            foreach (var item in storeRequestOrderInsert.productStoreRequestOrders)
            {
                ProductStoreRequestDetail productDetail = new ProductStoreRequestDetail();
                productDetail.StoreRequestOrderId = storeRequestOrderId;
                productDetail.ProductDetailId = item.ProductDetailId;
                productDetail.Quantity = item.Quantity;
                cartList.Add(productDetail);
            }
         /*   StoreRequestOrder customerOrder = new StoreRequestOrder(storeRequestOrderId,
                                                            storeRequestOrderInsert.StoreRequestId,
                                                            storeRequestOrderInsert.StoreSupplyCode,
                                                            cartList
                                                            );*/
        }
    }
}
