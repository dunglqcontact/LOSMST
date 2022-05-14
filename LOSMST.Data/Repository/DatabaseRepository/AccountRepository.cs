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
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        private readonly LOSMSTv01Context _dbContext;
        public AccountRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckEmailExsited(string emailValue)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Email == emailValue) != null;
        }

        public Account GetStoreManager(string storeCode)
        {
            var store = _dbContext.Stores.FirstOrDefault(s => s.Code == storeCode && s.StatusId == "1.1");
            var storeId = store.Id;
            Account account = _dbContext.Accounts.FirstOrDefault(a => a.StoreId== storeId && a.StatusId == "1.1" && (a.RoleId == "U03" || a.RoleId == "U04"));
            account.Store = null;
            return account;
        }
    }
}
