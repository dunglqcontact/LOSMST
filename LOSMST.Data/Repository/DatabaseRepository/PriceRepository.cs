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
using Microsoft.AspNetCore.Hosting;

namespace LOSMST.DataAccess.Repository.DatabaseRepository
{
    public class PriceRepository : GeneralRepository<Price>, IPriceRepository
    {
        private readonly LOSMSTv01Context _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PriceRepository(LOSMSTv01Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<int> ImportPriceToExcel(string fileUrl, string fileName)
        {
            List<int> errorRowList = new List<int>();
            DateTime currentDate = DateTime.Now.AddHours(7);
            var dateString = currentDate.ToString("yyMMdd");

            string countOrderEachDate = "00.##";

            string priceId = dateString + "";

            var checkPriceId = _dbContext.Prices.Where(x => x.Id.Contains(priceId));
            var currentActivePrice = _dbContext.Prices.Include(x => x.PriceDetails).FirstOrDefault(x => x.StatusId == "1.1");

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

            FileInfo fileInfo = new FileInfo(fileUrl);
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
                            if (worksheet.Cells[row, col].Value == null)
                            {
                                var checkErrorExisted = errorRowList.FirstOrDefault(x => x == row);
                                if (checkErrorExisted == 0)
                                {
                                    errorRowList.Add(row);
                                }
                                break;
                            }
                            else
                            {
                                priceDetail.ProductDetailId = worksheet.Cells[row, col].Value.ToString();
                            }
                        }
                        if(col == 6)
                        {
                            if (worksheet.Cells[row, col].Value == null)
                            {
                                var checkErrorExisted = errorRowList.FirstOrDefault(x => x == row);
                                if (checkErrorExisted == 0)
                                {
                                    errorRowList.Add(row);
                                }
                                break;
                            }
                            var text = worksheet.Cells[row, col].Value.ToString();
                            string numeric = new String(text.Where(Char.IsDigit).ToArray());
                            priceDetail.RetailPrice = double.Parse(numeric);
                        }
                        if (col == 7)
                        {
                            if (worksheet.Cells[row, col].Value == null)
                            {
                                var checkErrorExisted = errorRowList.FirstOrDefault(x => x == row);
                                if (checkErrorExisted == 0)
                                {
                                    errorRowList.Add(row);
                                }
                                break;
                            }
                            var text = worksheet.Cells[row, col].Value.ToString();
                            string numeric = new String(text.Where(Char.IsDigit).ToArray());
                            priceDetail.WholesalePrice = double.Parse(numeric);
                        }
                        if (priceDetail.ProductDetailId != null
                            && priceDetail.RetailPrice != 0
                            && priceDetail.WholesalePrice != 0)
                        {
                            priceDetail.PriceId = priceId;
                        }
                    }
                    if (priceDetail.PriceId == null)
                    {

                    }
                    else
                    {
                        priceDetails.Add(priceDetail);
                    }
                }
            }
            if (errorRowList.Count() == 0)
            {
                if (currentActivePrice != null)
                {
                    foreach (var activePrice in currentActivePrice.PriceDetails)
                    {
                        var checkPriceExisted = priceDetails.FirstOrDefault(x => x.ProductDetailId == activePrice.ProductDetailId);
                        if (checkPriceExisted == null)
                        {
                            PriceDetail newPriceDetail = new PriceDetail();
                            newPriceDetail.ProductDetailId = activePrice.ProductDetailId;
                            newPriceDetail.RetailPrice = activePrice.RetailPrice;
                            newPriceDetail.WholesalePrice = activePrice.WholesalePrice;
                            priceDetails.Add(newPriceDetail);
                        }
                    }
                    currentActivePrice.StatusId = "1.2";
                    currentActivePrice.EndDate = currentDate;
                    _dbContext.Prices.Update(currentActivePrice);
                }
                Price price = new Price();
                price.Id = priceId;
                price.StartDate = currentDate;
                price.PriceDetails = priceDetails;

                _dbContext.Prices.Add(price);
                return null;
            }
            else
            {
                return errorRowList;
            }
        }
    }
}
