using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper.SearchingModel;
using LOSMST.Models.Helper.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class ImportInventoryRepository : GeneralRepository<ImportInventory>, IImportInventoryRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public ImportInventoryRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ImportInventoryWithStoreSupplyViewModel> GetImportInventoryWithStoreSupply()
        {
            List<ImportInventoryWithStoreSupplyViewModel> importInventoryWithStoreSupplyViewModels = new List<ImportInventoryWithStoreSupplyViewModel>();
            var importInventories = _dbContext.ImportInventories.Include("ImportInventoryDetails.ProductDetail").Include(x => x.Store);
            foreach (var importInventory in importInventories)
            {
                ImportInventoryWithStoreSupplyViewModel inventoryWithStoreSupplyViewModel = new ImportInventoryWithStoreSupplyViewModel();
                inventoryWithStoreSupplyViewModel.ImportInventory = importInventory;
                importInventoryWithStoreSupplyViewModels.Add(inventoryWithStoreSupplyViewModel);
            }
            foreach (var item in importInventoryWithStoreSupplyViewModels)
            {
                var store = _dbContext.Stores.FirstOrDefault(x => x.Code == item.ImportInventory.ExportStoreCode);
                if (store != null)
                {
                    item.StoreSupply = store;
                }
            }
            return importInventoryWithStoreSupplyViewModels;
        }
    }
}
