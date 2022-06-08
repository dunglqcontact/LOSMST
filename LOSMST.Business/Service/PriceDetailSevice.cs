using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Business.Service
{
    public class PriceDetailService
    {
        private readonly IPriceDetailRepository _priceDetailRepository;

        public PriceDetailService(IPriceDetailRepository priceDetailRepository)
        {
            _priceDetailRepository = priceDetailRepository;
        }

        public IEnumerable<double> GetMinMaxPrice(int productId)
        {
            var values = _priceDetailRepository.GetCurrentPriceForProduct(productId);
            double max = values.Max(i => i.RetailPriceAfterTax);
            double min = values.Min(i => i.RetailPriceAfterTax);
            var data = new List<double>();
            data.Add(min);
            data.Add(max);
            return data;
        }

        public PagedList<ProductMinMaxPriceSearchHelper> GetAllProducts(ProductParameter productParam, PagingParameter paging)
        {
            var values = _priceDetailRepository.GetCurrentPriceForProduct();

            if (productParam.Id != null)
            {
                values = values.Where(x => x.product.Id == productParam.Id);
            }
            if (!string.IsNullOrWhiteSpace(productParam.Name))
            {
                values = values.Where(x => x.product.Name.Contains(productParam.Name, StringComparison.InvariantCultureIgnoreCase));
            }
            if (productParam.CategoryId != null)
            {
                values = values.Where(x => x.product.CategoryId == productParam.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(productParam.StatusId))
            {
                values = values.Where(x => x.product.StatusId == productParam.StatusId);
            }
            if (!string.IsNullOrWhiteSpace(productParam.sort))
            {
                switch (productParam.sort)
                {
                    case "Id":
                        if (productParam.dir == "asc")
                            values = values.OrderBy(d => d.product.Id);
                        else if (productParam.dir == "desc")
                            values = values.OrderByDescending(d => d.product.Id);
                        break;
                    case "Name":
                        if (productParam.dir == "asc")
                            values = values.OrderBy(d => d.product.Name);
                        else if (productParam.dir == "desc")
                            values = values.OrderByDescending(d => d.product.Name);
                        break;
                    case "CategoryId":
                        if (productParam.dir == "asc")
                            values = values.OrderBy(d => d.product.CategoryId);
                        else if (productParam.dir == "desc")
                            values = values.OrderByDescending(d => d.product.CategoryId);
                        break;
                    case "StatusId":
                        if (productParam.dir == "asc")
                            values = values.OrderBy(d => d.product.StatusId);
                        else if (productParam.dir == "desc")
                            values = values.OrderByDescending(d => d.product.StatusId);
                        break;
                }
            }

            return PagedList<ProductMinMaxPriceSearchHelper>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }

    }
}
