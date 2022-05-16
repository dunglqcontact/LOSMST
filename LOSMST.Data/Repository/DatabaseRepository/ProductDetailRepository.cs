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
    public  class ProductDetailRepository : GeneralRepository<ProductDetail>, IProductDetailRepository
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
    }
}
