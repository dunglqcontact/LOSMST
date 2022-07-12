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
    }
}