using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class StoreRequestOrderRepository : GeneralRepository<StoreRequestOrder>, IStoreRequestOrderRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public StoreRequestOrderRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
