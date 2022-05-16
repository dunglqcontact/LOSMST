using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using Microsoft.EntityFrameworkCore;
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
        internal DbSet<Account> _dbSet;
        public AccountRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Account>();
        }

        public bool CheckEmailExsited(string emailValue)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Email == emailValue) != null;
        }

        public void CreateLocalAccount(Account account)
        {
            string password = "12345678";
            var valueBytes = Encoding.UTF8.GetBytes(password);
            string passwordHass = Convert.ToBase64String(valueBytes);
            account.Password = passwordHass;
            _dbSet.Add(account);
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
