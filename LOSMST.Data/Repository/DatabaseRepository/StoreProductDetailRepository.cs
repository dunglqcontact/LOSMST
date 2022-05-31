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
    public class StoreProductDetailRepository : GeneralRepository<StoreProductDetail>, IStoreProductDetailRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public StoreProductDetailRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<StoreProductDetail> GetStoreProductDetail()
        {
            var values = _dbContext.StoreProductDetails.Include(a => a.ProductDetail.Product);
            return values.ToList();
        }
    }
}
