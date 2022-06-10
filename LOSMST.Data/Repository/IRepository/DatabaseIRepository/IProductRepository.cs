using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository
{
    public interface IProductRepository : GeneralIRepository<Product>
    {
        public IEnumerable<PriceDetail> GetCurrentPriceForProduct(int productId);
        public IEnumerable<Product> GetFavorite(List<int> listId, string includeProperties = null);
    }
}
