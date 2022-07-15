using LOSMST.Models.Helper.SearchingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository
{
    public interface IInventoryStatisticalRepository : GeneralIRepository<InventoryStatisticalViewModel>
    {
        public IEnumerable<InventoryStatisticalViewModel> GetInventoryStatisical(DateTime fromDate, DateTime toDate, int storeId);
    }
}
