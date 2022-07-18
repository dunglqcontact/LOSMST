using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Business.Service
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public PagedList<Account> GetAllAccounts(AccountParameter accountParam, PagingParameter paging)
        {
            var values = _accountRepository.GetAll(includeProperties: accountParam.includeProperties);

            if (accountParam.notIncludeRoleId != null)
            {
                foreach (var notInclude in accountParam.notIncludeRoleId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    values = values.Where(x => x.RoleId != notInclude);
                }
            }

            if (accountParam.Id != null)
            {
                values = values.Where(x => x.Id == accountParam.Id);
            }
            if (!string.IsNullOrWhiteSpace(accountParam.Fullname))
            {
                values = values.Where(x => x.Fullname.Contains(accountParam.Fullname, StringComparison.InvariantCultureIgnoreCase));
            }
            if (accountParam.StoreId != null)
            {
                values = values.Where(x => x.StoreId == accountParam.StoreId);
            }
            if (!string.IsNullOrWhiteSpace(accountParam.RoleId))
            {
                values = values.Where(x => x.RoleId == accountParam.RoleId);
            }
            if (!string.IsNullOrWhiteSpace(accountParam.Phone))
            {
                values = values.Where(x => x.Phone.Contains(accountParam.Phone, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(accountParam.StatusId))
            {
                values = values.Where(x => x.StatusId == accountParam.StatusId);
            }
            if (!string.IsNullOrWhiteSpace(accountParam.Email))
            {
                values = values.Where(x => x.Email.Contains(accountParam.Email,StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(accountParam.sort))
            {
                switch (accountParam.sort)
                {
                    case "id":
                        if (accountParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (accountParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                    case "fullname":
                        if (accountParam.dir == "asc")
                            values = values.OrderBy(d => d.Fullname);
                        else if (accountParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Fullname);
                        break;
                }
            }

            return PagedList<Account>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }

        public bool CheckEmaiExisted(string emailStr)
        {
            var values = _accountRepository.CheckEmailExsited(emailStr);

            return values;
        }

        public Account GetStoreManager(string storeCode)
        {
            var values = _accountRepository.GetStoreManager(storeCode);
            return values;
        }

        public Account GetUserBasicInfor(int id)
        {
            var values = _accountRepository.CheckAccountBasicInfor(id);
            return values;
        }

        public bool Add(Account account)
        {
            try
            {
                var abc = account;
                _accountRepository.CreateLocalAccount(account);
                
                _accountRepository.SaveDbChange();
                return true;
            }
            catch { return false; }
        }

        public bool Update(Account account)
        {
            try
            {
                _accountRepository.UpdateAccount(account);
                _accountRepository.SaveDbChange();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int UpdatePassword(int accountId, string currentPassword, string newPassword)
        {
            try
            {
                var values = _accountRepository.UpdatePassword(accountId, currentPassword, newPassword);
                _accountRepository.SaveDbChange();
                return values;
            }
            catch
            {
                return -2;
            }
        }
    }
}
