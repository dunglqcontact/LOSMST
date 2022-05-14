using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class RoleRepository : GeneralRepository<Role>, IRoleRepository
    {
        private readonly LOSMSTv01Context _dbContext;
        public RoleRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Role> GetStoreRole(int storeId)
        {
            var store = _dbContext.Stores.FirstOrDefault(s => s.Id == storeId);
            if (store != null) { 
                IQueryable<Role> roleList = _dbContext.Set<Role>();
                if (store.StoreCategoryId == "CHBL")
                {
                    roleList = roleList.Where(r => r.Id == "U03" || r.Id == "U05");
                }
                else if (store.StoreCategoryId == "CHCD")
                {
                    roleList = _dbContext.Roles.Where(r => r.Id == "U04" || r.Id == "U05");
                }
                return roleList.ToList();
            }
            return _dbContext.Set<Role>().ToList();
        }
    }
}
