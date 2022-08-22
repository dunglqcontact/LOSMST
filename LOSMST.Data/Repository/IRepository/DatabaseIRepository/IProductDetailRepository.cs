using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository
{
    public interface IProductDetailRepository : GeneralIRepository<ProductDetail>
    {
        public void AddProductDetail(ProductDetail productDetail);
        public ProductDetail CheckProductDetaiilExistence(int productId, string packageId, double volume);
        public Task<IEnumerable<ProductDetail>> GetProductDetailWithPrice(string includeProperties = null);
        public IEnumerable<ProductDetail> GetProductDetailByListId(List<string> listIdString);
        public IEnumerable<ProductDetail> GetProductDetailByListIdStoreManager(List<string> listIdString);
        public IEnumerable<ProductDetail> GetProductDetailAllWithPrice();
    }
}
