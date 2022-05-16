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
    public  class StoreRepository : GeneralRepository<Store>, IStoreRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public StoreRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CheckStoreManager(string storeCode, string roleId)
        {
            var store = _dbContext.Stores.FirstOrDefault(s => s.Code == storeCode && s.StatusId == "1.1");
            if (roleId == "U03" || roleId == "U04")
            {
                var account = _dbContext.Accounts.FirstOrDefault(a => (a.RoleId == "U03" || a.RoleId == "U04")
                                                            && a.StoreId == store.Id && a.StatusId == "1.1");
                return true;
            }
            return false;
        }

        public Store GetCurrentStoreByStoreCode(string storeCode)
        {
            var store = _dbContext.Stores.FirstOrDefault(s => s.Code == storeCode && s.StatusId == "1.1");
            return store;
        }

        public bool CheckCurrentStoreByStoreEmail(string storeEmail)
        {
            var store = _dbContext.Stores.FirstOrDefault(s => s.Email == storeEmail && s.StatusId == "1.1");
            return store != null;
        }
    }
}
