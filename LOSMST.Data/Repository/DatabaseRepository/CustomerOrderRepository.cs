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
    public class CustomerOrderRepository : GeneralRepository<CustomerOrder>, ICustomerOrderRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public CustomerOrderRepository(LOSMSTv01Context dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomerOrder InsertOrder(CustomerOrderInsertModel customerOrderInsert)
        {
            DateTime orderDateTime = DateTime.Now;
            DateTime estimateDeliveryDate = orderDateTime.AddDays(1);
            var dateString = orderDateTime.ToString("yyMMdd");

            string customerIdFormat = "00000.##";
            string countOrderEachDate = "000.##";

            string customerOrderId = dateString + "" + customerOrderInsert.CustomerId.ToString(customerIdFormat);

            var checkCustomerOrderIdTemp = _dbContext.CustomerOrders.Where(x => x.Id.Contains(customerOrderId));
            if (!IEnumerableCheckNull.IsAny(checkCustomerOrderIdTemp))
            {
                int count = 1;
                customerOrderId = customerOrderId + count.ToString(countOrderEachDate);
            }
            else
            {
                var lastCustomerOrder = checkCustomerOrderIdTemp.OrderBy(x => x.Id).Last();
                var id = lastCustomerOrder.Id;
                var lastOrderCount = id.Substring(11);
                var count = Int32.Parse(lastOrderCount) + 1;
                customerOrderId = customerOrderId + count.ToString(countOrderEachDate);
            }
            List<CustomerOrderDetail> cartList = new List<CustomerOrderDetail>();
            foreach (var item in customerOrderInsert.customerOrderDetails)
            {
                CustomerOrderDetail productDetail = new CustomerOrderDetail();
                productDetail.CustomerOrderId = customerOrderId;
                productDetail.ProductDetailId = item.ProductDetailId;
                productDetail.Quantity = item.Quantity;
                productDetail.Price = item.Price;
                cartList.Add(productDetail);
            }
            CustomerOrder customerOrder = new CustomerOrder(customerOrderId,
                                                            customerOrderInsert.TotalPrice,
                                                            customerOrderInsert.StoreId,
                                                            customerOrderInsert.CustomerId,
                                                            cartList
                                                            );
            _dbContext.Set<CustomerOrder>().Add(customerOrder);
            return customerOrder;
            //_dbContext.CustomerOrders.Add(customerOrder);
        }
        public void CancelCustomerOrder(string id, string reason)
        {
            var data = _dbContext.CustomerOrders.FirstOrDefault(x => x.Id == id);
            data.StatusId = "2.5";
            data.Reason = reason;
            _dbContext.CustomerOrders.Update(data);
        }
    }
}
