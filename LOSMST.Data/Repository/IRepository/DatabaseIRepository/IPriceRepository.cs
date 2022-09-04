using LOSMST.Models.Database;
using LOSMST.Models.Helper.SearchingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository
{
    public interface IPriceRepository : GeneralIRepository<Price>
    {
        public bool ImportPriceToExcel(string fileUrl, string fileName);
        public IEnumerable<ProductDetail> GetAllProductDetail();
    }
}
