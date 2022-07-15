using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository;
using LOSMST.Models.Helper.SearchingModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class InventoryStatisticalRepository : GeneralRepository<InventoryStatisticalViewModel>, IInventoryStatisticalRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public InventoryStatisticalRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<InventoryStatisticalViewModel> GetInventoryStatisical(DateTime fromDate, DateTime toDate, int storeId)
        {
            string importInclude = "ImportInventoryDetails.ProductDetail.Product";
            string exportInclude = "ExportInventoryDetails.ProductDetail.Product";
            var importListBefore = _dbContext.ImportInventories.Where(x => x.ImportDate < fromDate && x.StoreId == storeId)
                .Include(importInclude);


            var exportListBefore = _dbContext.ExportInventories.Where(x => x.ExportDate < fromDate && x.StoreId == storeId)
                .Include(exportInclude);

            List<InventoryStatisticalViewModel> inventory = new List<InventoryStatisticalViewModel>();

            foreach (var import in importListBefore)
            {
                foreach (var item in import.ImportInventoryDetails)
                {
                    var productDetail = item.ProductDetail;
                    var inventoryItem = inventory.FirstOrDefault(x => x.ProductDetailId == productDetail.Id);
                    if (inventoryItem != null)
                    {
                        inventoryItem.BeginingNumberPeriod += item.Quantity;
                    }
                    else
                    {
                        InventoryStatisticalViewModel inventoryStatisticalViewModel = new InventoryStatisticalViewModel();
                        inventoryStatisticalViewModel.ProductDetailId = productDetail.Id;
                        inventoryStatisticalViewModel.BeginingNumberPeriod = item.Quantity;
                        inventoryStatisticalViewModel.ProductName = item.ProductDetail.Product.Name;
                        inventory.Add(inventoryStatisticalViewModel);
                    }
                }
            }

            foreach (var export in exportListBefore)
            {
                foreach (var item in export.ExportInventoryDetails)
                {
                    var productDetail = item.ProductDetail;
                    var inventoryItem = inventory.FirstOrDefault(x => x.ProductDetailId == productDetail.Id);
                    if (inventoryItem != null)
                    {
                        inventoryItem.BeginingNumberPeriod -= item.Quantity;
                    }
                }
            }

            var importListInTerm = _dbContext.ImportInventories
                .Where(x => (x.ImportDate >= fromDate && x.ImportDate <= toDate ) && x.StoreId == storeId)
                .Include(importInclude);

            foreach (var import in importListInTerm)
            {
                foreach (var item in import.ImportInventoryDetails)
                {
                    var productDetail = item.ProductDetail;
                    var inventoryItem = inventory.FirstOrDefault(x => x.ProductDetailId == productDetail.Id);
                    if (inventoryItem != null)
                    {
                        inventoryItem.ImportInPeriod += item.Quantity;
                    }
                    else
                    {
                        InventoryStatisticalViewModel inventoryStatisticalViewModel = new InventoryStatisticalViewModel();
                        inventoryStatisticalViewModel.ProductDetailId = productDetail.Id;
                        inventoryStatisticalViewModel.ImportInPeriod = item.Quantity;
                        inventoryStatisticalViewModel.ProductName = item.ProductDetail.Product.Name;
                        inventory.Add(inventoryStatisticalViewModel);
                    }
                }
            }


            var exportListInTerm = _dbContext.ExportInventories
                .Where(x => (x.ExportDate >= fromDate && x.ExportDate <= toDate ) && x.StoreId == storeId)
                .Include(exportInclude);

            foreach (var export in exportListInTerm)
            {
                foreach (var item in export.ExportInventoryDetails)
                {
                    var productDetail = item.ProductDetail;
                    var inventoryItem = inventory.FirstOrDefault(x => x.ProductDetailId == productDetail.Id);
                    if (inventoryItem != null)
                    {
                        inventoryItem.ExportInPeriod += item.Quantity;
                    }
                }
            }
            

            return inventory;
        }
    }
}
