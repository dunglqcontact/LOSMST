using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class ProductRepository : GeneralRepository<Product>, IProductRepository
    {
        private readonly LOSMSTv01Context _dbContext;
        public ProductRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<PriceDetail> GetCurrentPriceForProduct(int productId)
        {
            var data = _dbContext.Prices.FirstOrDefault(x => x.StatusId == "1.1");
            var result = _dbContext.PriceDetails.Where(x => x.PriceId == data.Id);
            var values = result.Where(x => x.ProductDetail.ProductId == productId);
            foreach (var item in values)
            {
                var bc = item;
            }
            return values;
        }
    }
}
