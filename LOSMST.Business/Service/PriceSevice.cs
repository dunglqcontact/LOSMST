using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Business.Service
{
    public class PriceService
    {
        private readonly IPriceRepository _priceRepository;

        public PriceService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public Price ImportPrice(string fileUrl, string fileName)
        {
            try
            {
                var values = _priceRepository.ImportPriceToExcel(fileUrl, fileName);
                _priceRepository.SaveDbChange();
                return values;
            }catch (Exception ex)
            {
                return null;
            }
        }
    }
}
