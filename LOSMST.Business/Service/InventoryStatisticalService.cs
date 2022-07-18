using LOSMST.DataAccess.Repository.IRepository;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.SearchingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Business.Service
{
    public class InventoryStatisticalService
    {
        private readonly IInventoryStatisticalRepository _inventoryStatisticalRepository;

        public InventoryStatisticalService(IInventoryStatisticalRepository inventoryStatisticalRepository)
        {
            _inventoryStatisticalRepository = inventoryStatisticalRepository;
        }

        /*public PagedList<InventoryStatisticalViewModel> GetAllInventoryStatistical(DateTime fromDate, DateTime toDate, int storeId, PagingParameter paging)
        {
            var values = _inventoryStatisticalRepository.GetInventoryStatisical(fromDate, toDate, storeId);
            return PagedList<InventoryStatisticalViewModel>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }*/
        public IEnumerable<InventoryStatisticalViewModel> GetAllInventoryStatistical(DateTime fromDate, DateTime toDate, int storeId)
        {
            var values = _inventoryStatisticalRepository.GetInventoryStatisical(fromDate, toDate, storeId);
            return values;
        }
    }
}
