using LOSMST.Models.Database;
using LOSMST.Models.Helper.InsertHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository
{
    public interface IStoreRequestOrderRepository : GeneralIRepository<StoreRequestOrder>
    {
        public void InsertStoreRequestOrder(StoreRequestOrderInsertModel storeRequestOrderInsert);
        public void CancelStoreRequestOrder(string id, string reason);
        public void DenyStoreRequestOrder(string id, string reason);
        public IEnumerable<StoreRequestOrder> GetAllStoreRequestOrder(string includeProperties = null);
        public void ApproveStoreRequestOrder(StoreRequestOrder storeRequestOrderInput);
        public bool FinishStoreRequestOrder(string storeRequestOrderId);
    }
}
