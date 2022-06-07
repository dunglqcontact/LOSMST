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
    public class PriceDetailRepository : GeneralRepository<PriceDetail>, IPriceDetailRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public PriceDetailRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<PriceDetail> GetCurrentPriceForProduct(int productId)
        {
            var data = _dbContext.Prices.Include(x => x.PriceDetails).FirstOrDefault(x => x.StatusId == "1.1");
            var result = data.PriceDetails;
            var values = result.Where(x => x.ProductDetail.ProductId == productId);
            return values;
        }
    }
}
