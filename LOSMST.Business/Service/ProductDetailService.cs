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
    public class ProductDetailService
    {
        private readonly IProductDetailRepository _productDetailRepository;

        public ProductDetailService(IProductDetailRepository productDetailRepository)
        {
            _productDetailRepository = productDetailRepository;
        }

        public PagedList<ProductDetail> GetAllProductDetails(ProductDetailParameter productDetailParam, PagingParameter paging)
        {
            var values = _productDetailRepository.GetAll(includeProperties: productDetailParam.includeProperties);

            if (!string.IsNullOrWhiteSpace(productDetailParam.Id))
            {
                values = values.Where(x => x.Id == productDetailParam.Id);
            }
            if (productDetailParam.ProductId != null)
            {
                values = values.Where(x => x.ProductId == productDetailParam.ProductId);
            }
            if (!string.IsNullOrWhiteSpace(productDetailParam.StatusId))
            {
                values = values.Where(x => x.StatusId == productDetailParam.StatusId);
            }
            if (!string.IsNullOrWhiteSpace(productDetailParam.PackageId))
            {
                values = values.Where(x => x.PackageId == productDetailParam.PackageId);
            }
            if (!string.IsNullOrWhiteSpace(productDetailParam.sort))
            {
                switch (productDetailParam.sort)
                {
                    case "Id":
                        if (productDetailParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (productDetailParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                    case "ProductId":
                        if (productDetailParam.dir == "asc")
                            values = values.OrderBy(d => d.ProductId);
                        else if (productDetailParam.dir == "desc")
                            values = values.OrderByDescending(d => d.ProductId);
                        break;
                    case "PackageId":
                        if (productDetailParam.dir == "asc")
                            values = values.OrderBy(d => d.PackageId);
                        else if (productDetailParam.dir == "desc")
                            values = values.OrderByDescending(d => d.PackageId);
                        break;
                    case "Volume":
                        if (productDetailParam.dir == "asc")
                            values = values.OrderBy(d => d.Volume);
                        else if (productDetailParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Volume);
                        break;
                    case "QuantityWholeSalePrice":
                        if (productDetailParam.dir == "asc")
                            values = values.OrderBy(d => d.WholeSalePriceQuantity);
                        else if (productDetailParam.dir == "desc")
                            values = values.OrderByDescending(d => d.WholeSalePriceQuantity);
                        break;
                }
            }

            return PagedList<ProductDetail>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }
        public bool Add(ProductDetail productDetail)
        {
            try
            {
                _productDetailRepository.AddProductDetail(productDetail);

                _productDetailRepository.SaveDbChange();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(string productDetailId)
        {
            try
            {
                _productDetailRepository.Remove(productDetailId);
                _productDetailRepository.SaveDbChange();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(ProductDetail productDetail)
        {
            try
            {
                _productDetailRepository.Update(productDetail);
                _productDetailRepository.SaveDbChange();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
