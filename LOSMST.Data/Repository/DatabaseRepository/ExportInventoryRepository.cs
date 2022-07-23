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
    public class ExportInventoryRepository : GeneralRepository<ExportInventory>, IExportInventoryRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public ExportInventoryRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<ExportInventoryWithStoreSupplyViewModel> GetExportInventoryWithStoreRequest()
        {
            List<ExportInventoryWithStoreSupplyViewModel> exportInventoryWithStoreSupplyViewModels = new List<ExportInventoryWithStoreSupplyViewModel>();
            var exportInventories = _dbContext.ExportInventories.Include("ExportInventoryDetails.ProductDetail").Include(x => x.Store);
            foreach (var exportInventory in exportInventories)
            {
                ExportInventoryWithStoreSupplyViewModel exportWithStoreSupplyViewModel = new ExportInventoryWithStoreSupplyViewModel();
                exportWithStoreSupplyViewModel.Inventory = exportInventory;
                exportInventoryWithStoreSupplyViewModels.Add(exportWithStoreSupplyViewModel);
            }
            foreach (var item in exportInventoryWithStoreSupplyViewModels)
            {
                var store = _dbContext.Stores.FirstOrDefault(x => x.Code == item.Inventory.StoreImportCode);
                if (store != null)
                {
                    item.StoreRequest = store;
                }
            }
            return exportInventoryWithStoreSupplyViewModels;
        }
    }
}
