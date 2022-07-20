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
    public class ImportInventoryService
    {
        private readonly IImportInventoryRepository _importInventoryRepository;

        public ImportInventoryService(IImportInventoryRepository importInventoryRepository)
        {
            _importInventoryRepository = importInventoryRepository;
        }

        public PagedList<ImportInventory> GetAllImportInventory(ImportInventoryParameter importInventoryParam, PagingParameter paging)
        {
            var values = _importInventoryRepository
                .GetAll(includeProperties: "ImportInventoryDetails,Store");

            foreach(var importInventory in values)
            {
                importInventory.Store.ImportInventories = null;
            }

            if (importInventoryParam.Id != null)
            {
                values = values.Where(x => x.Id == importInventoryParam.Id);
            }

            if (importInventoryParam.StoreId != null)
            {
                values = values.Where(x => x.StoreId == importInventoryParam.StoreId);
            }

            if(importInventoryParam.FromDate != null && importInventoryParam.ToDate != null)
            {
                string fromDateStr = "0" + importInventoryParam.FromDate.ToString();
                fromDateStr = fromDateStr.Substring(0, 10);
                if (fromDateStr.Substring(9) == " ")
                {
                    fromDateStr = fromDateStr.Substring(0, 3) + "0" + fromDateStr.Substring(3) + "00:00:00";
                }
                else
                {
                    fromDateStr += " 00:00:00";
                }

                string toDateStr = "0" + importInventoryParam.ToDate.ToString();
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
                    .Where(x => x.ImportDate >= importInventoryParam.FromDate && x.ImportDate <= importInventoryParam.ToDate);
            }

            if (!string.IsNullOrWhiteSpace(importInventoryParam.sort))
            {
                switch (importInventoryParam.sort)
                {
                    case "id":
                        if (importInventoryParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (importInventoryParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                    case "importDate":
                        if (importInventoryParam.dir == "asc")
                            values = values.OrderBy(d => d.ImportDate);
                        else if (importInventoryParam.dir == "desc")
                            values = values.OrderByDescending(d => d.ImportDate);
                        break;
                }
            }
            return PagedList<ImportInventory>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }
    }
}