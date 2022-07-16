using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper.SearchingModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class ProductStoreRequestDetailRepository : GeneralRepository<ProductStoreRequestDetail>, IProductStoreRequestDetailRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public ProductStoreRequestDetailRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductStoreRequestDetailInventoryViewModel> GetProductStoreRequestDetailInventoryViewModels(string storeRequestOrderId)
        {
            var storeRequestOrder = _dbContext.StoreRequestOrders.FirstOrDefault(x => x.Id == storeRequestOrderId);
            Store? storeSupplyOrder = _dbContext.Stores.FirstOrDefault(x => x.Code == storeRequestOrder.StoreSupplyCode);
            List<ProductStoreRequestDetailInventoryViewModel> viewModelList = new List<ProductStoreRequestDetailInventoryViewModel>();
            var productStoreRequestDetail = _dbContext.ProductStoreRequestDetails.Where(x => x.StoreRequestOrderId == storeRequestOrderId).Include(x => x.ProductDetail.Product);
            foreach (var item in productStoreRequestDetail)
            {
                ProductStoreRequestDetailInventoryViewModel viewModel = new ProductStoreRequestDetailInventoryViewModel();
                viewModel.ProductStoreRequestDetail = item;
                
                viewModelList.Add(viewModel);
            }
            foreach (var item in viewModelList)
            {
                item.ProductStoreRequestDetail.ProductDetail.Product.ProductDetails = null;
                item.CurrentQuantity = _dbContext.StoreProductDetails.FirstOrDefault(x => x.ProductDetailId == item.ProductStoreRequestDetail.ProductDetailId && x.StoreId == storeSupplyOrder.Id).CurrentQuantity;
                item.ProductStoreRequestDetail.ProductDetail.StoreProductDetails = null;
                item.ProductStoreRequestDetail.StoreRequestOrder.ProductStoreRequestDetails = null;
            }
            return viewModelList;
        }
    }
}
