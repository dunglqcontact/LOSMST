using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Business.Service
{
    public class ProductStoreRequestDetailService
    {
        private readonly IProductStoreRequestDetailRepository _productStoreRequestDetailRepository;

        public ProductStoreRequestDetailService(IProductStoreRequestDetailRepository productStoreRequestDetailRepository)
        {
            _productStoreRequestDetailRepository = productStoreRequestDetailRepository;
        }

        public IEnumerable<ProductStoreRequestDetailInventoryViewModel> GetProductStoreRequestDetailInventoryViewModels(string storeRequestOrderId)
        {
            var data = _productStoreRequestDetailRepository.GetProductStoreRequestDetailInventoryViewModels(storeRequestOrderId);
            return data;
        }

        public IEnumerable<ProductStoreRequestDetailInventoryViewModel> GetProductStoreRequestDetailStoreRequestInventoryViewModels(string storeRequestOrderId)
        {
            var data = _productStoreRequestDetailRepository.GetProductStoreRequestDetailStoreRequestInventoryViewModels(storeRequestOrderId);
            return data;
        }
    }
}
