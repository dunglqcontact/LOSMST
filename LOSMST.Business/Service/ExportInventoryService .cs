using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Business.Service
{
    public class ExportInventoryService
    {
        private readonly IExportInventoryRepository _exportInventoryRepository;

        public ExportInventoryService(IExportInventoryRepository exportInventoryRepository)
        {
            _exportInventoryRepository = exportInventoryRepository;
        }

        public PagedList<ExportInventoryWithStoreSupplyViewModel> exportInventories(ExportInventoryParameter exportInventoryParam, PagingParameter paging)
        {
            var values = _exportInventoryRepository
                .GetExportInventoryWithStoreRequest();
            foreach (var inventory in values)
            {
                inventory.Inventory.Store.ExportInventories = null;
                foreach (var item in inventory.Inventory.ExportInventoryDetails)
                {
                    item.ProductDetail.ExportInventoryDetails = null;
                    item.ProductDetail.Product.ProductDetails = null;
                }
            }
            if (exportInventoryParam.Id != null)
            {
                values = values.Where(x => x.Inventory.Id == exportInventoryParam.Id);
            }

            if (exportInventoryParam.StoreId != null)
            {
                values = values.Where(x => x.Inventory.StoreId == exportInventoryParam.StoreId);
            }

            if (exportInventoryParam.FromDate != null && exportInventoryParam.ToDate != null)
            {
                string fromDateStr = "0" + exportInventoryParam.FromDate.ToString();
                fromDateStr = fromDateStr.Substring(0, 10);
                if (fromDateStr.Substring(9) == " ")
                {
                    fromDateStr = fromDateStr.Substring(0, 3) + "0" + fromDateStr.Substring(3) + "00:00:00";
                }
                else
                {
                    fromDateStr += " 00:00:00";
                }

                string toDateStr = "0" + exportInventoryParam.ToDate.ToString();
                toDateStr = toDateStr.Substring(0, 10);
                if (toDateStr.Substring(9) == " ")
                {
                    toDateStr = toDateStr.Substring(0, 3) + "0" + toDateStr.Substring(3) + "23:59:59";
                }
                else
                {
                    toDateStr += " 23:59:59";
                }

                DateTime fromDate = DateTime.ParseExact(fromDateStr, "MM/dd/yyyy HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture);
                DateTime toDate = DateTime.ParseExact(toDateStr, "MM/dd/yyyy HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture);
                values = values
                    .Where(x => x.Inventory.ExportDate >= exportInventoryParam.FromDate && x.Inventory.ExportDate <= exportInventoryParam.ToDate);
            }

            if (!string.IsNullOrWhiteSpace(exportInventoryParam.sort))
            {
                switch (exportInventoryParam.sort)
                {
                    case "id":
                        if (exportInventoryParam.dir == "asc")
                            values = values.OrderBy(d => d.Inventory.Id);
                        else if (exportInventoryParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Inventory.Id);
                        break;
                    case "exportDate":
                        if (exportInventoryParam.dir == "asc")
                            values = values.OrderBy(d => d.Inventory.ExportDate);
                        else if (exportInventoryParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Inventory.ExportDate);
                        break;
                }
            }
            return PagedList<ExportInventoryWithStoreSupplyViewModel>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }
    }
}