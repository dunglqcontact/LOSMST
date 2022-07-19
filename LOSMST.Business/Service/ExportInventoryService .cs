using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
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

        public PagedList<ExportInventory> exportInventories(ExportInventoryParameter exportInventoryParam, PagingParameter paging)
        {
            var values = _exportInventoryRepository
                .GetAll(includeProperties: "ExportInventoryDetails,Store");
            foreach (var item in values)
            {
                item.Store.ExportInventories = null;
            }
            if (exportInventoryParam.Id != null)
            {
                values = values.Where(x => x.Id == exportInventoryParam.Id);
            }

            if (exportInventoryParam.StoreId != null)
            {
                values = values.Where(x => x.StoreId == exportInventoryParam.StoreId);
            }

            if (exportInventoryParam.FromDate != null && exportInventoryParam.ToDate != null)
            {
                string fromDateStr = "0" + exportInventoryParam.FromDate.Value;
                fromDateStr = fromDateStr.Substring(0, 10) + " 00:00:00";
                string toDateStr = "0" + exportInventoryParam.ToDate.Value;
                toDateStr = toDateStr.Substring(0, 10) + " 23:59:59";
                DateTime fromDate = DateTime.ParseExact(fromDateStr, "MM/dd/yyyy HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture);
                DateTime toDate = DateTime.ParseExact(toDateStr, "MM/dd/yyyy HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture);
                values = values
                    .Where(x => x.ExportDate >= exportInventoryParam.FromDate && x.ExportDate <= exportInventoryParam.ToDate);
            }

            if (!string.IsNullOrWhiteSpace(exportInventoryParam.sort))
            {
                switch (exportInventoryParam.sort)
                {
                    case "id":
                        if (exportInventoryParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (exportInventoryParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                    case "exportDate":
                        if (exportInventoryParam.dir == "asc")
                            values = values.OrderBy(d => d.ExportDate);
                        else if (exportInventoryParam.dir == "desc")
                            values = values.OrderByDescending(d => d.ExportDate);
                        break;
                }
            }
            return PagedList<ExportInventory>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }
    }
}