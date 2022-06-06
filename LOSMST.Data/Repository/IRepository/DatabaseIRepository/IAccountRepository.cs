﻿using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository
{
    public interface IAccountRepository : GeneralIRepository<Account>
    {
        public bool CheckEmailExsited(string emailValue);
        public Account GetStoreManager(string storeCode);
        public void UpdatePassword(int accountId, string password);
        public void CreateLocalAccount(Account account);
    }
}
