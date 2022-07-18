using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper.InsertHelper;
using LOSMST.Models.Helper.SearchingModel;
using LOSMST.Models.Helper.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class CustomerOrderDetailRepository : GeneralRepository<CustomerOrderDetail>, ICustomerOrderDetailRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public CustomerOrderDetailRepository(LOSMSTv01Context dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CustomerOrderDetail> GetAllCustomerOrderDetail()
        {
            var data = _dbContext.CustomerOrderDetails.Include(x => x.ProductDetail.Package).Include(x => x.ProductDetail.Product);
            return data.ToList();
        }

        public IEnumerable<CustomerOrderDetailInventoryViewModel> GetProductStoreRequestDetailInventoryViewModels(string customerOrderId)
        {
            var customerOrder = _dbContext.CustomerOrders.FirstOrDefault(x => x.Id == customerOrderId);
            List<CustomerOrderDetailInventoryViewModel> viewModelList = new List<CustomerOrderDetailInventoryViewModel>();
            var customerOrderDetails = _dbContext.CustomerOrderDetails.Where(x => x.CustomerOrderId == customerOrderId).Include(x => x.ProductDetail.Package).Include(x => x.ProductDetail.Product);
            foreach (var item in customerOrderDetails)
            {
                CustomerOrderDetailInventoryViewModel viewModel = new CustomerOrderDetailInventoryViewModel();
                viewModel.CustomerOrderDetail = item;

                viewModelList.Add(viewModel);
            }
            foreach (var item in viewModelList)
            {
                var storeProductDetail = _dbContext.StoreProductDetails.FirstOrDefault(x => x.ProductDetailId == item.CustomerOrderDetail.ProductDetailId && x.StoreId == customerOrder.StoreId);
                if (storeProductDetail == null)
                {
                    item.CurrentQuantity = 0;
                }
                else {
                    item.CurrentQuantity = storeProductDetail.CurrentQuantity;
                    item.CustomerOrderDetail.CustomerOrder.CustomerOrderDetails = null;
                    item.CustomerOrderDetail.ProductDetail.Package.ProductDetails = null;
                    item.CustomerOrderDetail.ProductDetail.Product.ProductDetails = null;
                }
            }
            return viewModelList;
        }
    }
}
