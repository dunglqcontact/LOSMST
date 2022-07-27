using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository
{
    public interface IStoreRepository : GeneralIRepository<Store>
    {
        public bool CheckStoreManager(string storeCode, string roleId);
        public Store GetCurrentStoreByStoreCode(string storeCode);
        public bool CheckCurrentStoreByStoreEmail(string storeEmail);
        public IEnumerable<Store> GetStoreSort();
    }
}
