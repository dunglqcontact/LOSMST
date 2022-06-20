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
    public class CustomerOrderDetailRepository : GeneralRepository<CustomerOrderDetail>, ICustomerOrderDetailRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public CustomerOrderDetailRepository(LOSMSTv01Context dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
