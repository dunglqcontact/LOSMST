using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper.SearchingModel;
using LOSMST.Models.Helper.Utils;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class PriceRepository : GeneralRepository<Price>, IPriceRepository
    {
        private readonly LOSMSTv01Context _dbContext;

        public PriceRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Price ImportPriceToExcel(string fileUrl, string fileName)
        {
            DateTime currentDate = DateTime.Now;
            var dateString = currentDate.ToString("yyMMdd");

            string countOrderEachDate = "00.##";

            string priceId = dateString + "";

            var checkPriceId = _dbContext.Prices.Where(x => x.Id.Contains(priceId));
            if (!IEnumerableCheckNull.IsAny(checkPriceId))
            {
                int count = 1;

                priceId = priceId + count.ToString(countOrderEachDate);
            }
            else
            {
                var lastStoreRequestOrder = checkPriceId.OrderBy(x => x.Id).Last();
                var id = lastStoreRequestOrder.Id;
                var lastOrderCount = id.Substring(6);
                var count = Int32.Parse(lastOrderCount) + 1;
                priceId = priceId + count.ToString(countOrderEachDate);
            }

            List<PriceDetail> priceDetails = new List<PriceDetail>();

            var filePath = fileUrl + "\\" + fileName;
            FileInfo fileInfo = new FileInfo(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    PriceDetail priceDetail = new PriceDetail();
                    for (int col = 1; col <= colCount; col++)
                    {
                        if(col == 1)
                        {
                            priceDetail.ProductDetailId = worksheet.Cells[row,col].Value.ToString();
                        }
                        if(col == 6)
                        {
                            priceDetail.RetailPrice = double.Parse(worksheet.Cells[row, col].Value.ToString());
                        }
                        if (col == 7)
                        {
                            priceDetail.WholesalePrice = double.Parse(worksheet.Cells[row, col].Value.ToString());
                        }
                        if (priceDetail.ProductDetailId != null
                            && priceDetail.RetailPrice != 0
                            && priceDetail.WholesalePrice != 0)
                        {
                            priceDetail.PriceId = priceDetail.ProductDetailId.ToString();
                        }
                    }
                    priceDetails.Add(priceDetail);
                }
            }
            Price price = new Price();
            price.Id = priceId;
            price.StartDate = currentDate;
            price.PriceDetails = priceDetails;

            _dbContext.Prices.Add(price);

            return price;
        }
    }
}
