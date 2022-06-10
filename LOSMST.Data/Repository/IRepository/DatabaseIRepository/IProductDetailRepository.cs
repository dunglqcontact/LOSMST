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
        public IEnumerable<ProductDetail> GetProductDetailWithPrice();
        public IEnumerable<ProductDetail> GetProductDetailByListId(List<string> listIdString);
    }
}
