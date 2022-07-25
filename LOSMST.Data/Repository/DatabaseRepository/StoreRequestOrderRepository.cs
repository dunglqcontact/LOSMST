using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper.InsertHelper;
using LOSMST.Models.Helper.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class StoreRequestOrderRepository : GeneralRepository<StoreRequestOrder>, IStoreRequestOrderRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public StoreRequestOrderRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        // Generate Store Request Order Id template
        static string StoreRequestOrderIdTempGen(StoreRequestOrderInsertModel storeRequestOrderInsert)
        {
            DateTime orderDateTime = DateTime.Now.AddHours(7);
            var dateString = orderDateTime.ToString("yyMMdd");

            string storeRequestId = "000.##";

            string storeRequestOrderId = dateString + "" + storeRequestOrderInsert.StoreRequestId.ToString(storeRequestId)
                                        + storeRequestOrderInsert.StoreSupplyCode;
            storeRequestOrderId = storeRequestOrderId.ToUpper();
            return storeRequestOrderId;
        }

        static string GenerateStoreRequestOrderId(IEnumerable<StoreRequestOrder> storeRequestOrders, string storeRequestOrderId)
        {
            string countOrderEachDate = "00.##";
            if (!IEnumerableCheckNull.IsAny(storeRequestOrders))
            {
                int count = 1;

                storeRequestOrderId = storeRequestOrderId + count.ToString(countOrderEachDate);
            }
            else
            {
                var lastStoreRequestOrder = storeRequestOrders.OrderBy(x => x.Id).Last();
                var id = lastStoreRequestOrder.Id;
                var lastOrderCount = id.Substring(10);
                var count = Int32.Parse(lastOrderCount) + 1;
                storeRequestOrderId = storeRequestOrderId + count.ToString(countOrderEachDate);
            }
            return storeRequestOrderId;
        }

        static List<ProductStoreRequestDetail> CreateCartList(StoreRequestOrderInsertModel storeRequestOrderInsert, string storeRequestOrderId)
        {
            List<ProductStoreRequestDetail> cartList = new List<ProductStoreRequestDetail>();
            foreach (var item in storeRequestOrderInsert.productStoreRequestOrders)
            {
                ProductStoreRequestDetail productDetail = new ProductStoreRequestDetail();
                productDetail.StoreRequestOrderId = storeRequestOrderId;
                productDetail.ProductDetailId = item.ProductDetailId;
                productDetail.Quantity = item.Quantity;
                cartList.Add(productDetail);
            }
            return cartList;
        }

        public void InsertStoreRequestOrder(StoreRequestOrderInsertModel storeRequestOrderInsert)
        {
            DateTime orderDateTime = DateTime.Now.AddHours(7);
            var dateString = orderDateTime.ToString("yyMMdd");

            string storeRequestId = "00.##";
            string countOrderEachDate = "00.##";

            string storeRequestOrderId = dateString + "" + storeRequestOrderInsert.StoreRequestId.ToString(storeRequestId)
                                        + storeRequestOrderInsert.StoreSupplyCode;


            var checkStoreRequestOrderIdTemp = _dbContext.StoreRequestOrders.Where(x => x.Id.Contains(storeRequestOrderId));
            if (!IEnumerableCheckNull.IsAny(checkStoreRequestOrderIdTemp))
            {
                int count = 1;

                storeRequestOrderId = storeRequestOrderId + count.ToString(countOrderEachDate);
            }
            else
            {
                var lastStoreRequestOrder = checkStoreRequestOrderIdTemp.OrderBy(x => x.Id).Last();
                var id = lastStoreRequestOrder.Id;
                var lastOrderCount = id.Substring(12);
                var count = Int32.Parse(lastOrderCount) + 1;
                storeRequestOrderId = storeRequestOrderId + count.ToString(countOrderEachDate);
            }
            
            List<ProductStoreRequestDetail> cartList = new List<ProductStoreRequestDetail>();
            foreach (var item in storeRequestOrderInsert.productStoreRequestOrders)
            {
                ProductStoreRequestDetail productDetail = new ProductStoreRequestDetail();
                productDetail.StoreRequestOrderId = storeRequestOrderId;
                productDetail.ProductDetailId = item.ProductDetailId;
                productDetail.Quantity = item.Quantity;
                cartList.Add(productDetail);
            }
            /*            var storeRequestOrderId = StoreRequestOrderIdTempGen(storeRequestOrderInsert);

                        var checkStoreRequestOrderIdTemp = _dbContext.StoreRequestOrders.Where(x => x.Id.Contains(storeRequestOrderId));

                        storeRequestOrderId = GenerateStoreRequestOrderId(checkStoreRequestOrderIdTemp, storeRequestOrderId);

                        var cartList = CreateCartList(storeRequestOrderInsert, storeRequestOrderId);*/

            StoreRequestOrder storeRequestOrder = new StoreRequestOrder(orderDateTime,
                                                            storeRequestOrderId,
                                                            storeRequestOrderInsert.StoreRequestId,
                                                            storeRequestOrderInsert.StoreSupplyCode,
                                                            cartList
                                                            );
            //_dbContext.StoreRequestOrders.Add(storeRequestOrder);
            _dbContext.Set<StoreRequestOrder>().Add(storeRequestOrder);
        }

        public void CancelStoreRequestOrder(string id, string reason)
        {
            var data = _dbContext.StoreRequestOrders.FirstOrDefault(x => x.Id == id);
            data.StatusId = "2.5";
            data.Reason = reason;
            _dbContext.StoreRequestOrders.Update(data);
        }

        public void DenyStoreRequestOrder(string id, string reason)
        {
            var storeRequestOrder = _dbContext.StoreRequestOrders.Include(x => x.ProductStoreRequestDetails).FirstOrDefault(x => x.Id == id);
            var storeSupply = _dbContext.Stores.FirstOrDefault(x => x.Code == storeRequestOrder.StoreSupplyCode && x.StatusId == "1.1");
            if (storeRequestOrder.StatusId == "2.1")
            {
                storeRequestOrder.StatusId = "2.4";
                storeRequestOrder.Reason = reason;
            }
            else if (storeRequestOrder.StatusId == "2.2")
            {
                storeRequestOrder.StatusId = "2.4";
                storeRequestOrder.Reason = reason;
                foreach (var item in storeRequestOrder.ProductStoreRequestDetails)
                {
                    var storeInventory = _dbContext.StoreProductDetails
                        .FirstOrDefault(x => x.ProductDetailId == item.ProductDetailId && x.StoreId == storeSupply.Id);
                    storeInventory.CurrentQuantity += item.Quantity;
                    _dbContext.StoreProductDetails.Update(storeInventory);
                }
            }
            _dbContext.StoreRequestOrders.Update(storeRequestOrder);
        }

        public IEnumerable<StoreRequestOrder> GetAllStoreRequestOrder(string includeProperties = null)
        {
            IQueryable<StoreRequestOrder> data = _dbContext.StoreRequestOrders.Include("ProductStoreRequestDetails.ProductDetail.Product");
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    data = data.Include(includeProp);
                }
            }
            return data;
        }

        public void ApproveStoreRequestOrder(StoreRequestOrder storeRequestOrderInput)
        {
            if(storeRequestOrderInput != null)
            {
                if(storeRequestOrderInput.ProductStoreRequestDetails != null)
                {
                    var storeRequestOrder = _dbContext.StoreRequestOrders.Include(x => x.StoreRequest).FirstOrDefault(x => x.Id == storeRequestOrderInput.Id);
                    var storeSupply = _dbContext.Stores.FirstOrDefault(x => x.Code == storeRequestOrder.StoreSupplyCode && x.StatusId == "1.1");
                    storeRequestOrder.EstimatedReceiveDate = storeRequestOrderInput.EstimatedReceiveDate;
                    storeRequestOrder.StatusId = "2.2";
                    _dbContext.StoreRequestOrders.Update(storeRequestOrder);
                    var productStoreRequestDetailList = _dbContext.ProductStoreRequestDetails.Include(x => x.ProductDetail)
                                                        .Where(x => x.StoreRequestOrderId == storeRequestOrderInput.Id);
                    if (productStoreRequestDetailList != null)
                    {
                        List<ProductStoreRequestDetail> updatelist = new List<ProductStoreRequestDetail>();
                        foreach (var item in productStoreRequestDetailList)
                        {
                            if (!storeRequestOrderInput.ProductStoreRequestDetails.Any(x => x.Id == item.Id))
                            {
                                _dbContext.ProductStoreRequestDetails.Remove(item);
                            }
                            else
                            {
                                updatelist.Add(item);
                                if (item.ProductDetail.PackageId != "P")
                                {
                                    item.Quantity = storeRequestOrderInput.ProductStoreRequestDetails.FirstOrDefault(x => x.Id == item.Id).Quantity;
                                }
                                else
                                {
                                    var quantity = storeRequestOrderInput.ProductStoreRequestDetails.FirstOrDefault(x => x.Id == item.Id).Quantity;
                                    var volume = item.ProductDetail.Volume;
                                    item.Quantity = (int)(quantity * volume);
                                }
                                _dbContext.ProductStoreRequestDetails.Update(item);
                            }
                        }
                        foreach (var item in updatelist)
                        {
                            var stroreSupplyInventory = _dbContext.StoreProductDetails.FirstOrDefault(x => x.ProductDetailId == item.ProductDetailId && x.StoreId == storeSupply.Id);
                            if (stroreSupplyInventory != null)
                            {
                                if (storeSupply.Code != "XNBL")
                                {
                                    if (stroreSupplyInventory.CurrentQuantity >= item.Quantity)
                                    {
                                        var currentQuantity = stroreSupplyInventory.CurrentQuantity - item.Quantity;
                                        stroreSupplyInventory.CurrentQuantity = currentQuantity;
                                        _dbContext.StoreProductDetails.Update(stroreSupplyInventory);
                                    }
                                }
                            }
                        }

                        DateTime orderDateTime = DateTime.Now.AddHours(7);
                        var dateString = orderDateTime.ToString("yyMMdd");

                        string storeIdFormat = "00.##";
                        string countOrderEachDateFormat = "00.##";

                        var storeSupplyOrder = _dbContext.Stores.FirstOrDefault(x => x.Code == storeRequestOrder.StoreSupplyCode && x.StatusId == "1.1");
                        string exportId = dateString + "" + storeSupplyOrder.Id.ToString(storeIdFormat);

                        var checkExportInventory = _dbContext.ExportInventories.Where(x => x.Id.Contains(exportId));

                        if (!IEnumerableCheckNull.IsAny(checkExportInventory))
                        {
                            int count = 1;

                            exportId = exportId + count.ToString(countOrderEachDateFormat);
                        }
                        else
                        {
                            var lastExport = checkExportInventory.OrderBy(x => x.Id).Last();
                            var id = lastExport.Id;
                            var lastOrderCount = id.Substring(8);
                            var count = Int32.Parse(lastOrderCount) + 1;
                            exportId = exportId + count.ToString(countOrderEachDateFormat);
                        }



                        List<ExportInventoryDetail> exportInventoryDetails = new List<ExportInventoryDetail>();

                        foreach (var item in storeRequestOrder.ProductStoreRequestDetails)
                        {
                            ExportInventoryDetail exportInventoryDetail = new ExportInventoryDetail();

                            exportInventoryDetail.ExportInventoryId = exportId;
                            exportInventoryDetail.ProductDetailId = item.ProductDetailId;
                            exportInventoryDetail.Quantity = item.Quantity;
                            exportInventoryDetails.Add(exportInventoryDetail);
                        }

                        ExportInventory exportInventory = new ExportInventory();
                        exportInventory.Id = exportId;
                        exportInventory.ExportDate = orderDateTime;
                        exportInventory.ExportInventoryDetails = exportInventoryDetails;
                        exportInventory.StoreId = storeSupplyOrder.Id;
                        exportInventory.StoreImportCode = storeRequestOrder.StoreRequest.Code;

                        _dbContext.ExportInventories.Add(exportInventory);
                    }
                }
            }
        }

        public bool FinishStoreRequestOrder(string storeRequestOrderId)
        {
            DateTime orderDateTime = DateTime.Now.AddHours(7);
            var dateString = orderDateTime.ToString("yyMMdd");
            var storeRequestOrder = _dbContext.StoreRequestOrders.Include(x => x.ProductStoreRequestDetails).FirstOrDefault(x => x.Id == storeRequestOrderId);
            if (storeRequestOrder != null)
            {
                if (storeRequestOrder.StatusId == "2.2")
                {
                    storeRequestOrder.StatusId = "2.3";
                    storeRequestOrder.ReceiveDate = orderDateTime;

                    _dbContext.StoreRequestOrders.Update(storeRequestOrder);

                    string storeIdFormat = "00.##";
                    string countOrderEachDateFormat = "00.##";

                    string importId = dateString + "" + storeRequestOrder.StoreRequestId.ToString(storeIdFormat);



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

                    List<ImportInventoryDetail> importInventoryDetails = new List<ImportInventoryDetail>();
                    foreach (var item in storeRequestOrder.ProductStoreRequestDetails)
                    {
                        ImportInventoryDetail importInventoryDetail = new ImportInventoryDetail();
                        importInventoryDetail.ImportInventoryId = importId;
                        importInventoryDetail.ProductDetailId = item.ProductDetailId;
                        importInventoryDetail.Quantity = item.Quantity;
                        importInventoryDetails.Add(importInventoryDetail);


                        //find store product detail
                        var storeProductDetail = _dbContext
                            .StoreProductDetails
                            .FirstOrDefault(x => x.ProductDetailId == item.ProductDetailId && x.StoreId == storeRequestOrder.StoreRequestId);

                        //store product detail attribute
                        if (storeProductDetail == null)
                        {
                            StoreProductDetail newStoreProductDetail = new StoreProductDetail();
                            newStoreProductDetail.StoreId = storeRequestOrder.StoreRequestId;
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
                    ImportInventory importInventory = new ImportInventory();
                    importInventory.Id = importId;
                    importInventory.ImportDate = orderDateTime;
                    importInventory.ImportInventoryDetails = importInventoryDetails;
                    importInventory.StoreId = storeRequestOrder.StoreRequestId;
                    importInventory.ExportStoreCode = storeRequestOrder.StoreSupplyCode;
                    _dbContext.ImportInventories.Add(importInventory);

                    return true;
                }
            }
            return false;
        }

    }
}
