using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.SearchingModel
{
    public class ImportInventoryWithStoreSupplyViewModel
    {
        public ImportInventory importInventory { get; set; }
        public Store StoreSupply { get; set; }
    }
}
