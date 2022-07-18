using LOSMST.Models.Database;
using LOSMST.Models.Helper.InsertHelper;
using LOSMST.Models.Helper.SearchingModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository
{
    public interface ICustomerOrderDetailRepository : GeneralIRepository<CustomerOrderDetail>
    {
        public IEnumerable<CustomerOrderDetail> GetAllCustomerOrderDetail();
        public IEnumerable<CustomerOrderDetailInventoryViewModel> GetProductStoreRequestDetailInventoryViewModels(string customerOrderId);
    }
}
