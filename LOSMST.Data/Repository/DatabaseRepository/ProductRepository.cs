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
            var values = result.Where(x => x.ProductDetail.ProductId == productId && x.ProductDetail.StatusId == "3.1");

            return values;
        }

        public IEnumerable<Product> GetFavorite(List<int> listId, string includeProperties = null)
        {
            var data = _dbContext.Products.Where(x => listId.Contains(x.Id) && x.StatusId == "3.1");
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    data = data.Include(includeProp);
                }
            }
            return data;
        }
    }
}
