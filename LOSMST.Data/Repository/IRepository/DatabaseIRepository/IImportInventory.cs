using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository
{
    public interface IImportInventoryRepository : GeneralIRepository<ImportInventory>
    {
        public bool CreateAnImportInventory(string storeRequestOrderId, ImportInventory importInventory);
    }
}
