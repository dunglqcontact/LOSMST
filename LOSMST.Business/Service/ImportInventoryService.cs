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

        public PagedList<ImportInventory> GetAllAccounts(ImportInventoryParameter importInventoryParam, PagingParameter paging)
        {
            var values = _importInventoryRepository
                .GetAll(includeProperties: "ImportInventoryDetails");

            if (importInventoryParam.Id != null)
            {
                values = values.Where(x => x.Id == importInventoryParam.Id);
            }

            if (importInventoryParam.StoreId != null)
            {
                values = values.Where(x => x.StoreId == importInventoryParam.StoreId);
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