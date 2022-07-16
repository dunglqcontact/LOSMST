using LOSMST.Models.Database;
using LOSMST.Models.Helper.SearchingModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository
{
    public interface IProductStoreRequestDetailRepository : GeneralIRepository<ProductStoreRequestDetail>
    {
        public IEnumerable<ProductStoreRequestDetailInventoryViewModel> GetProductStoreRequestDetailInventoryViewModels(string storeRequestOrderId);
        public IEnumerable<ProductStoreRequestDetailInventoryViewModel> GetProductStoreRequestDetailStoreRequestInventoryViewModels(string storeRequestOrderId);
    }
}
