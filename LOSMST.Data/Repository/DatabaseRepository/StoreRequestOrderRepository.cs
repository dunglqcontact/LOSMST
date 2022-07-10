using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper.InsertHelper;
using LOSMST.Models.Helper.Utils;
using Microsoft.EntityFrameworkCore;
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
        // Generate Store Request Order Id template
        static string StoreRequestOrderIdTempGen(StoreRequestOrderInsertModel storeRequestOrderInsert)
        {
            DateTime orderDateTime = DateTime.Now;
            var dateString = orderDateTime.ToString("yyMMdd");

            string storeRequestId = "000.##";

            string storeRequestOrderId = dateString + "" + storeRequestOrderInsert.StoreRequestId.ToString(storeRequestId)
                                        + storeRequestOrderInsert.StoreSupplyCode;
            storeRequestOrderId = storeRequestOrderId.ToUpper();
            return storeRequestOrderId;
        }

        static string GenerateStoreRequestOrderId(IEnumerable<StoreRequestOrder> storeRequestOrders, string storeRequestOrderId)
        {
            string countOrderEachDate = "00.##";
            if (!IEnumerableCheckNull.IsAny(storeRequestOrders))
            {
                int count = 1;

                storeRequestOrderId = storeRequestOrderId + count.ToString(countOrderEachDate);
            }
            else
            {
                var lastStoreRequestOrder = storeRequestOrders.OrderBy(x => x.Id).Last();
                var id = lastStoreRequestOrder.Id;
                var lastOrderCount = id.Substring(10);
                var count = Int32.Parse(lastOrderCount) + 1;
                storeRequestOrderId = storeRequestOrderId + count.ToString(countOrderEachDate);
            }
            return storeRequestOrderId;
        }

        static List<ProductStoreRequestDetail> CreateCartList(StoreRequestOrderInsertModel storeRequestOrderInsert, string storeRequestOrderId)
        {
            List<ProductStoreRequestDetail> cartList = new List<ProductStoreRequestDetail>();
            foreach (var item in storeRequestOrderInsert.productStoreRequestOrders)
            {
                ProductStoreRequestDetail productDetail = new ProductStoreRequestDetail();
                productDetail.StoreRequestOrderId = storeRequestOrderId;
                productDetail.ProductDetailId = item.ProductDetailId;
                productDetail.Quantity = item.Quantity;
                cartList.Add(productDetail);
            }
            return cartList;
        }

        public void InsertStoreRequestOrder(StoreRequestOrderInsertModel storeRequestOrderInsert)
        {
            DateTime orderDateTime = DateTime.Now;
            var dateString = orderDateTime.ToString("yyMMdd");

            string storeRequestId = "00.##";
            string countOrderEachDate = "00.##";

            string storeRequestOrderId = dateString + "" + storeRequestOrderInsert.StoreRequestId.ToString(storeRequestId)
                                        + storeRequestOrderInsert.StoreSupplyCode;


            var checkStoreRequestOrderIdTemp = _dbContext.StoreRequestOrders.Where(x => x.Id.Contains(storeRequestOrderId));
            if (!IEnumerableCheckNull.IsAny(checkStoreRequestOrderIdTemp))
            {
                int count = 1;

                storeRequestOrderId = storeRequestOrderId + count.ToString(countOrderEachDate);
            }
            else
            {
                var lastStoreRequestOrder = checkStoreRequestOrderIdTemp.OrderBy(x => x.Id).Last();
                var id = lastStoreRequestOrder.Id;
                var lastOrderCount = id.Substring(13);
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
            /*            var storeRequestOrderId = StoreRequestOrderIdTempGen(storeRequestOrderInsert);

                        var checkStoreRequestOrderIdTemp = _dbContext.StoreRequestOrders.Where(x => x.Id.Contains(storeRequestOrderId));

                        storeRequestOrderId = GenerateStoreRequestOrderId(checkStoreRequestOrderIdTemp, storeRequestOrderId);

                        var cartList = CreateCartList(storeRequestOrderInsert, storeRequestOrderId);*/

            StoreRequestOrder storeRequestOrder = new StoreRequestOrder(storeRequestOrderId,
                                                            storeRequestOrderInsert.StoreRequestId,
                                                            storeRequestOrderInsert.StoreSupplyCode,
                                                            cartList
                                                            );
            //_dbContext.StoreRequestOrders.Add(storeRequestOrder);
            _dbContext.Set<StoreRequestOrder>().Add(storeRequestOrder);
        }

        public void CancelStoreRequestOrder(string id, string reason)
        {
            var data = _dbContext.StoreRequestOrders.FirstOrDefault(x => x.Id == id);
            data.StatusId = "2.5";
            data.Reason = reason;
            _dbContext.StoreRequestOrders.Update(data);
        }

        public void DenyStoreRequestOrder(string id, string reason)
        {
            var data = _dbContext.StoreRequestOrders.FirstOrDefault(x => x.Id == id);
            data.StatusId = "2.4";
            data.Reason = reason;
            _dbContext.StoreRequestOrders.Update(data);
        }

/*        public void FinishStoreRequestOrder(string id, string reason)
        {
            var data = _dbContext.StoreRequestOrders.FirstOrDefault(x => x.Id == id);
            data.StatusId = "2.3";
            data.Reason = reason;
            _dbContext.StoreRequestOrders.Update(data);
        }*/

        public IEnumerable<StoreRequestOrder> GetAllStoreRequestOrder(string includeProperties = null)
        {
            IQueryable<StoreRequestOrder> data = _dbContext.StoreRequestOrders.Include("ProductStoreRequestDetails.ProductDetail.Product");
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    data = data.Include(includeProp);
                }
            }
            return data;
        }

        public void ApproveStoreRequestOrder(StoreRequestOrder storeRequestOrderInput)
        {
            if(storeRequestOrderInput != null)
            {
                if(storeRequestOrderInput.ProductStoreRequestDetails != null)
                {
                    var storeRequestOrder = _dbContext.StoreRequestOrders.FirstOrDefault(x => x.Id == storeRequestOrderInput.Id);
                    storeRequestOrder.EstimatedReceiveDate = storeRequestOrderInput.EstimatedReceiveDate;
                    storeRequestOrder.StatusId = "2.2";
                    _dbContext.StoreRequestOrders.Update(storeRequestOrder);
                    var productStoreRequestDetailList = _dbContext.ProductStoreRequestDetails
                                                        .Where(x => x.StoreRequestOrderId == storeRequestOrderInput.Id);
                    if (productStoreRequestDetailList != null)
                    {
                        foreach (var item in productStoreRequestDetailList)
                        {
                            if (!storeRequestOrderInput.ProductStoreRequestDetails.Any(x => x.Id == item.Id))
                            {
                                _dbContext.ProductStoreRequestDetails.Remove(item);
                            }
                            else
                            {
                                item.Quantity = storeRequestOrderInput.ProductStoreRequestDetails.FirstOrDefault(x => x.Id == item.Id).Quantity;
                                _dbContext.ProductStoreRequestDetails.Update(item);
                            }
                        }
                    }
                 //   _dbContext.StoreRequestOrders.Update(storeRequestOrderInput);

                }
            }
        }
    }
}
