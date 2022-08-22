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
    public class CustomerOrderRepository : GeneralRepository<CustomerOrder>, ICustomerOrderRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public CustomerOrderRepository(LOSMSTv01Context dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomerOrder InsertOrder(CustomerOrderInsertModel customerOrderInsert)
        {
            DateTime orderDateTime = DateTime.Now.AddHours(7);
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
                                                            cartList,
                                                            orderDateTime
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
            data.ReceiveDate = DateTime.Now.AddHours(7);
            _dbContext.CustomerOrders.Update(data);
        }

        public IEnumerable<Account> GetCustomerAccountByName(string fullname)
        {
            var values = _dbContext.Accounts.Where(x => x.Fullname.Contains(fullname, StringComparison.InvariantCultureIgnoreCase));
            return values;
        }

        public void ApproveCustomerOrder(string customerOrderId, DateTime? estimatedReceiveDateStr, int? managerAccountId)
        {
           /* DateTime myDate = DateTime.ParseExact(estimatedReceiveDateStr, "yyyy-MM-dd hh:mm tt",
                                       System.Globalization.CultureInfo.InvariantCulture);*/
            var customerOrder = _dbContext.CustomerOrders
                .Include(x => x.CustomerOrderDetails)
                .FirstOrDefault(x => x.Id == customerOrderId);
            
            foreach (var item in customerOrder.CustomerOrderDetails)
            {
                var storeSelling = _dbContext.StoreProductDetails
                    .FirstOrDefault(x => x.ProductDetailId == item.ProductDetailId && x.StoreId == customerOrder.StoreId);
                if (storeSelling != null)
                {
                    if(storeSelling.CurrentQuantity >= item.Quantity)
                    {
                        var currentQuantity = storeSelling.CurrentQuantity - item.Quantity;
                        storeSelling.CurrentQuantity = currentQuantity;
                        _dbContext.StoreProductDetails.Update(storeSelling);
                    }
                }
            }
            customerOrder.StatusId = "2.2";
            customerOrder.EstimatedReceiveDate = estimatedReceiveDateStr;
            customerOrder.ManagerAccountId = managerAccountId;
            _dbContext.CustomerOrders.Update(customerOrder);

            DateTime receiveDate = DateTime.Now.AddHours(7);
            var dateString = receiveDate.ToString("yyMMdd");

            string storeIdFormat = "00.##";
            string countOrderEachDateFormat = "00.##";


            string exportId = dateString + "" + customerOrder.StoreId?.ToString(storeIdFormat);


            var checkExportInventory = _dbContext.ExportInventories.Where(x => x.Id.Contains(exportId));

            if (!IEnumerableCheckNull.IsAny(checkExportInventory))
            {
                int count = 1;

                exportId = exportId + count.ToString(countOrderEachDateFormat);
            }
            else
            {
                var lastExport = checkExportInventory.OrderBy(x => x.Id).Last();
                var id = lastExport.Id;
                var lastOrderCount = id.Substring(8);
                var count = Int32.Parse(lastOrderCount) + 1;
                exportId = exportId + count.ToString(countOrderEachDateFormat);
            }

            List<ExportInventoryDetail> exportInventoryDetails = new List<ExportInventoryDetail>();
            foreach (var item in customerOrder.CustomerOrderDetails)
            {
                ExportInventoryDetail detail = new ExportInventoryDetail();
                detail.Quantity = item.Quantity;
                detail.Price = item.Price;
                detail.ProductDetailId = item.ProductDetailId;
                detail.ExportInventoryId = exportId;
                exportInventoryDetails.Add(detail);
            }
            ExportInventory exportInventory = new ExportInventory();
            exportInventory.Id = exportId;
            exportInventory.ExportDate = receiveDate;
            exportInventory.ExportInventoryDetails = exportInventoryDetails;
            exportInventory.StoreId = (int) customerOrder.StoreId;
            _dbContext.ExportInventories.Add(exportInventory);
        }

        public void DenyCustomerOrder(string id, string reason)
        {
            var data = _dbContext.CustomerOrders.FirstOrDefault(x => x.Id == id);
            data.StatusId = "2.4";
            data.Reason = reason;
            data.ReceiveDate = DateTime.Now.AddHours(7);
            _dbContext.CustomerOrders.Update(data);
        }

        public void FinishCustomerOrder(string customerOrderId, int staffAccountId)
        {
            DateTime receiveDate = DateTime.Now.AddHours(7);
            var dateString = receiveDate.ToString("yyMMdd");
            var customerOrder = _dbContext.CustomerOrders.Include(x => x.CustomerOrderDetails).FirstOrDefault(x => x.Id == customerOrderId);
            customerOrder.StatusId = "2.3";
            customerOrder.ReceiveDate =  receiveDate;
            customerOrder.StaffAccountId = staffAccountId;
            _dbContext.CustomerOrders.Update(customerOrder);
        }
    }
}
