using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Business.Service
{
    public class PriceDetailService
    {
        private readonly IPriceDetailRepository _priceDetailRepository;

        public PriceDetailService(IPriceDetailRepository priceDetailRepository)
        {
            _priceDetailRepository = priceDetailRepository;
        }

        public IEnumerable<double> GetMinMaxPrice(int productId)
        {
            var values = _priceDetailRepository.GetCurrentPriceForProduct(productId);
            double max = values.Max(i => i.RetailPriceAfterTax);
            double min = values.Min(i => i.RetailPriceAfterTax);
            var data = new List<double>();
            data.Add(min);
            data.Add(max);
            return data;
        }
    }
}
