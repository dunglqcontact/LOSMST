using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Business.Service
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public PagedList<Product> GetAllProducts(ProductParameter productParam, PagingParameter paging)
        {
            var values = _productRepository.GetAll(includeProperties: productParam.includeProperties);

            if (productParam.Id != null)
            {
                values = values.Where(x => x.Id == productParam.Id);
            }
            if (!string.IsNullOrWhiteSpace(productParam.Name))
            {
                values = values.Where(x => x.Name.Contains(productParam.Name, StringComparison.InvariantCultureIgnoreCase));
            }
            if (productParam.CategoryId != null)
            {
                values = values.Where(x => x.CategoryId == productParam.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(productParam.StatusId))
            {
                values = values.Where(x => x.StatusId == productParam.StatusId);
            }
            if (!string.IsNullOrWhiteSpace(productParam.sort))
            {
                switch (productParam.sort)
                {
                    case "Id":
                        if (productParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (productParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                    case "Name":
                        if (productParam.dir == "asc")
                            values = values.OrderBy(d => d.Name);
                        else if (productParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Name);
                        break;
                    case "CategoryId":
                        if (productParam.dir == "asc")
                            values = values.OrderBy(d => d.CategoryId);
                        else if (productParam.dir == "desc")
                            values = values.OrderByDescending(d => d.CategoryId);
                        break;
                    case "StatusId":
                        if (productParam.dir == "asc")
                            values = values.OrderBy(d => d.StatusId);
                        else if (productParam.dir == "desc")
                            values = values.OrderByDescending(d => d.StatusId);
                        break;
                }
            }

            return PagedList<Product>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }

        public bool Add(Product product)
        {
            try
            {
                var data = product;
                _productRepository.Add(product);
                
                _productRepository.SaveDbChange();
                return true;
            }
            catch { return false; }
        }

        public bool Update(Product product)
        {
            try
            {
                _productRepository.Update(product);
                _productRepository.SaveDbChange();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DisableProduct(int productId)
        {
            try
            {
                var data = _productRepository.GetFirstOrDefault(x => x.Id == productId, includeProperties: "ProductDetails");
                data.StatusId = "3.2";
                foreach (var productDetail in data.ProductDetails)
                {
                    productDetail.StatusId = "3.2";
                }
                _productRepository.Update(data);
                _productRepository.SaveDbChange();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
