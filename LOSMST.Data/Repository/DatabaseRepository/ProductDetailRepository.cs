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
    public class ProductDetailRepository : GeneralRepository<ProductDetail>, IProductDetailRepository
    {
        private readonly LOSMSTv01Context _dbContext;
        internal DbSet<ProductDetail> _dbSet;

        public ProductDetailRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ProductDetail>();
        }

        public void AddProductDetail(ProductDetail productDetail)
        {
            var productId = productDetail.ProductId;
            var packageId = productDetail.PackageId;
            var volume = productDetail.Volume * 100;

            string productIdFmt = "0000.##";
            string volumeFmt = "00000.##";

            string productDetailId = productId.ToString(productIdFmt) + packageId.ToUpper() + volume.ToString(volumeFmt);
            productDetail.Id = productDetailId;
            _dbSet.Add(productDetail);
        }

        public ProductDetail CheckProductDetaiilExistence(int productId, string packageId, double volume)
        {
            var productDetail = _dbContext.ProductDetails.FirstOrDefault(x => x.ProductId == productId && x.PackageId
                                                                                    == packageId && x.Volume == volume);
            if (productDetail != null)
            {
                return productDetail;
            }
            return null;
        }

        public IEnumerable<ProductDetail> GetProductDetailWithPrice()
        {
            var price = _dbContext.Prices.FirstOrDefault(x => x.StatusId == "1.1");
            var productDetails = _dbContext.ProductDetails.Where(x => x.PriceDetails.Count != 0)
                                .Include(x => x.PriceDetails.Where(x => x.PriceId == price.Id));
            return productDetails;
        }

        public IEnumerable<ProductDetail> GetProductDetailByListId(List<string> listIdString)
        {
            var price = _dbContext.Prices.FirstOrDefault(x => x.StatusId == "1.1");

            var data = _dbContext.ProductDetails.Where(x => x.StatusId == "3.1" && listIdString.Contains(x.Id) && x.PriceDetails.Count != 0)
                                                    .Include(x => x.PriceDetails.Where(x => x.PriceId == price.Id));
            foreach (var item in data)
            {
                var priceDetail = item.PriceDetails;
            }
            return data;
        }
    }
}
