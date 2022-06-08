using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper.SearchingModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class PriceDetailRepository : GeneralRepository<PriceDetail>, IPriceDetailRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public PriceDetailRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductMinMaxPriceSearchHelper> GetCurrentPriceForProduct()
        {
            List<ProductMinMaxPriceSearchHelper> metadata = new List<ProductMinMaxPriceSearchHelper>();
            var data = _dbContext.Prices.Include(x => x.PriceDetails).FirstOrDefault(x => x.StatusId == "1.1");
            var result = data.PriceDetails;
            var products = _dbContext.Set<Product>();
            foreach (var item in products)
            {
                var values = result.Where(x => x.ProductDetail.ProductId == item.Id);
                List<double> priceData = new List<double>();
                var min = values.Min(x => x.RetailPriceAfterTax);
                var max = values.Max(x => x.RetailPriceAfterTax);
                priceData.Add(min);
                priceData.Add(max);
                metadata.Add(new ProductMinMaxPriceSearchHelper
                {
                    product = item,
                    MinMaxPrice = priceData,
                });
            }
            
            return metadata;
        }

        public IEnumerable<PriceDetail> GetCurrentPriceForProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
