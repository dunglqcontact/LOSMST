using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Business.Service
{
    public class ProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public PagedList<ProductCategory> GetAllProductCategories(ProductCategoryParameter productCategoryParam, PagingParameter paging)
        {
            var values = _productCategoryRepository.GetAll(includeProperties: productCategoryParam.includeProperties);

            if (productCategoryParam.Id != null)
            {
                values = values.Where(x => x.Id == productCategoryParam.Id);
            }
            if (!string.IsNullOrWhiteSpace(productCategoryParam.Name))
            {
                values = values.Where(x => x.Name.Contains(productCategoryParam.Name, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(productCategoryParam.sort))
            {
                switch (productCategoryParam.sort)
                {
                    case "Id":
                        if (productCategoryParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (productCategoryParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                    case "Name":
                        if (productCategoryParam.dir == "asc")
                            values = values.OrderBy(d => d.Name);
                        else if (productCategoryParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Name);
                        break;
                }
            }

            return PagedList<ProductCategory>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }
    }
}
