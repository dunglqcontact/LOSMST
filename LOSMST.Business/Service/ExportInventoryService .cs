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

        public PagedList<ExportInventory> GetAllAccounts(ExportInventoryParameter exportInventoryParam, PagingParameter paging)
        {
            var values = _exportInventoryRepository
                .GetAll(includeProperties: "ExportInventoryDetails");

            if (exportInventoryParam.Id != null)
            {
                values = values.Where(x => x.Id == exportInventoryParam.Id);
            }

            if (exportInventoryParam.StoreId != null)
            {
                values = values.Where(x => x.StoreId == exportInventoryParam.StoreId);
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