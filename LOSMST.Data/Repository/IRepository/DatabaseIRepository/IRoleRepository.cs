using LOSMST.Models.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository
{
    public interface IRoleRepository : GeneralIRepository<Role>
    {
        public IEnumerable<Role> GetStoreRole(int storeId);
    }
}
