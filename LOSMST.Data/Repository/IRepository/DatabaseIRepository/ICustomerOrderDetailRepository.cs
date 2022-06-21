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
    public interface ICustomerOrderDetailRepository : GeneralIRepository<CustomerOrderDetail>
    {
        public IEnumerable<CustomerOrderDetail> GetAllCustomerOrderDetail();
    }
}
