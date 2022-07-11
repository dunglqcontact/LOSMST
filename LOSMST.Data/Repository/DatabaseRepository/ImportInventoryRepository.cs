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

        public bool CreateAnImportInventory(string storeRequestOrderId, ImportInventory importInventory)
        {
            DateTime orderDateTime = DateTime.Now;
            var dateString = orderDateTime.ToString("yyMMdd");
            var storeRequestOrder = _dbContext.StoreRequestOrders.FirstOrDefault(x => x.Id == storeRequestOrderId);
            if (storeRequestOrder != null)
            {
                if (storeRequestOrder.StatusId == "2.2")
                {
                    storeRequestOrder.StatusId = "2.3";
                    storeRequestOrder.ReceiveDate = orderDateTime;

                    _dbContext.StoreRequestOrders.Update(storeRequestOrder);

                    string storeIdFormat = "00.##";
                    string countOrderEachDateFormat = "00.##";

                    string importId = dateString + "" + importInventory.StoreId.ToString(storeIdFormat);

                    var checkImportInventoryId = _dbContext.ImportInventories.Where(x => x.Id.Contains(importId));
                    if (!IEnumerableCheckNull.IsAny(checkImportInventoryId))
                    {
                        int count = 1;

                        importId = importId + count.ToString(countOrderEachDateFormat);
                    }
                    else
                    {
                        var lastImport = checkImportInventoryId.OrderBy(x => x.Id).Last();
                        var id = lastImport.Id;
                        var lastOrderCount = id.Substring(8);
                        var count = Int32.Parse(lastOrderCount) + 1;
                        importId = importId + count.ToString(countOrderEachDateFormat);
                    }
                    importInventory.ImportDate = orderDateTime;
                    List<ImportInventoryDetail> importInventoryDetails = new List<ImportInventoryDetail>();
                    foreach (var item in importInventory.ImportInventoryDetails)
                    {
                        item.ImportInventoryId = importId;
                        importInventoryDetails.Add(item);

                        //find store product detail
                        var storeProductDetail = _dbContext
                            .StoreProductDetails
                            .FirstOrDefault(x => x.ProductDetailId == item.ProductDetailId && x.StoreId == importInventory.StoreId);

                        //store product detail attribute
                        if (storeProductDetail == null)
                        {
                            StoreProductDetail newStoreProductDetail = new StoreProductDetail();
                            newStoreProductDetail.StoreId = importInventory.StoreId;
                            newStoreProductDetail.ProductDetailId = item.ProductDetailId;
                            newStoreProductDetail.CurrentQuantity = item.Quantity;
                            _dbContext.StoreProductDetails.Add(newStoreProductDetail);
                        }
                        else
                        {
                            storeProductDetail.CurrentQuantity += item.Quantity;
                            _dbContext.StoreProductDetails.Update(storeProductDetail);
                        }
                    }
                    importInventory.Id = importId;
                    importInventory.ImportDate = orderDateTime;
                    importInventory.ImportInventoryDetails = importInventoryDetails;
                    _dbContext.ImportInventories.Add(importInventory);
                    return true;
                }
            }
            return false;
        }
    }
}
