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
            string password = "petrolimex";
            var valueBytes = Encoding.UTF8.GetBytes(password);
            string passwordHass = Convert.ToBase64String(valueBytes);
            account.Password = passwordHass;
            _dbSet.Add(account);
        }

        public int UpdatePassword(int accountId, string currentPassword, string newPassword)
        {
            if (!newPassword.Equals("") && !currentPassword.Equals(""))
            {
                if (newPassword != currentPassword)
                {
                    var account = _dbContext.Accounts.FirstOrDefault(x => x.Id == accountId);
                    if (account != null)
                    {
                        var encodeCurrentPassword = Encoding.UTF8.GetBytes(currentPassword);
                        string currentPasswordHass = Convert.ToBase64String(encodeCurrentPassword);
                        if (currentPasswordHass == account.Password)
                        {
                            var valueBytesEncode = Encoding.UTF8.GetBytes(newPassword);
                            string passwordHass = Convert.ToBase64String(valueBytesEncode);
                            account.Password = passwordHass;
                            _dbSet.Update(account);
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }
            return -2;
        }

        public Account GetStoreManager(string storeCode)
        {
            var store = _dbContext.Stores.FirstOrDefault(s => s.Code == storeCode && s.StatusId == "1.1");
            var storeId = store.Id;
            Account account = _dbContext.Accounts.FirstOrDefault(a => a.StoreId == storeId && a.StatusId == "1.1" && (a.RoleId == "U03" || a.RoleId == "U04"));
            account.Store = null;
            return account;
        }
        public Account CheckAccountBasicInfor(int accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId && a.StatusId == "1.1");
            if (account.Fullname != null && account.Phone != null)
            { 
                return account; 
            }
            return null;
        }

        public bool UpdateAccount(Account account)
        {
            var currentAccount = _dbContext.Accounts.FirstOrDefault(a => a.Id == account.Id);
            if(currentAccount != null)
            {
                currentAccount.Fullname = account.Fullname;
                currentAccount.Dob = account.Dob;
                currentAccount.Avatar = account.Avatar;
                currentAccount.Address = account.Address;
                currentAccount.Phone = account.Phone;
                currentAccount.District = account.District;
                currentAccount.Ward = account.Ward;
                currentAccount.Gender = account.Gender;
                _dbContext.Accounts.Update(currentAccount);
                return true;
            }
            return false;
        }
    }
}
