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
    public interface ICustomerOrderRepository : GeneralIRepository<CustomerOrder>
    {
        public CustomerOrder InsertOrder(CustomerOrderInsertModel customerOrder);
        public void CancelCustomerOrder(string id, string reason);
        public void ApproveCustomerOrder(string id, DateTime? estimatedReceiveDateStr);
        public void DenyCustomerOrder(string id, string reason);
        public void FinishCustomerOrder(string id);
    }
}
