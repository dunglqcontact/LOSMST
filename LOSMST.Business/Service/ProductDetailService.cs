using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.SearchingModel;
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
            if (productDetailParam.includeProperties != null)
            {
                if(productDetailParam.includeProperties.Contains("Product"))
                {
                    if (productDetailParam.CategoryId != 0)
                    {
                        values = values.Where(x => x.Product.CategoryId == productDetailParam.CategoryId);
                    }
                }
            }
            

            if (!string.IsNullOrWhiteSpace(productDetailParam.includeProperties))
            {
                if (productDetailParam.includeProperties.Contains("Product"))
                {
                    foreach (var productDetail in values)
                    {
                        productDetail.Product.ProductDetails = null;
                    }
                }
            }

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

        public PagedList<ProductDetail> GetAllProductDetailWithPrice(ProductDetailParameter productDetailParam, PagingParameter paging)
        {
            var values = _productDetailRepository.GetProductDetailAllWithPrice();
            foreach (var item in values)
            {
                item.Package.ProductDetails = null;
                item.Product.ProductDetails = null;
            }
            foreach (var productDetail in values)
            {
                if (productDetail.PriceDetails != null)
                {
                    for (int i = 0; i < productDetail.PriceDetails.Count; i++)
                    {
                        productDetail.PriceDetails.ElementAt(i).Price = null;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(productDetailParam.includeProperties))
            {
                if (productDetailParam.includeProperties.Contains("Product"))
                {
                    foreach (var productDetail in values)
                    {
                        productDetail.Product.ProductDetails = null;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(productDetailParam.Id))
            {
                values = values.Where(x => x.Id == productDetailParam.Id);
            }
            if (productDetailParam.ProductId != null)
            {
                values = values.Where(x => x.ProductId == productDetailParam.ProductId);
            }
            if (!string.IsNullOrWhiteSpace(productDetailParam.PackageId))
            {
                values = values.Where(x => x.PackageId == productDetailParam.PackageId);
            }
            if (!string.IsNullOrWhiteSpace(productDetailParam.ProductName))
            {
                values = values.Where(x => x.Product.Name.Contains(productDetailParam.ProductName, StringComparison.InvariantCultureIgnoreCase));
            }
            if (productDetailParam.CategoryId != null)
            {
                values = values.Where(x => x.Product.CategoryId == productDetailParam.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(productDetailParam.StatusId))
            {
                values = values.Where(x => x.StatusId == productDetailParam.StatusId);
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
                    case "category-id":
                        if (productDetailParam.dir == "asc")
                            values = values.OrderBy(d => d.Product.CategoryId);
                        else if (productDetailParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Product.CategoryId);
                        break;
                }
            }

            return PagedList<ProductDetail>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }

        public IEnumerable<ProductDetail> GetAllProductDetailWithPriceNonPaging(ProductDetailParameter productDetailParam, PagingParameter paging)
        {
            var values = _productDetailRepository.GetProductDetailAllWithPrice();
            foreach (var item in values)
            {
                item.Package.ProductDetails = null;
                item.Product.ProductDetails = null;
            }
            values = values.Where(x => x.StatusId == "3.1");
            foreach (var productDetail in values)
            {
                if (productDetail.PriceDetails != null)
                {
                    for (int i = 0; i < productDetail.PriceDetails.Count; i++)
                    {
                        productDetail.PriceDetails.ElementAt(i).Price = null;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(productDetailParam.includeProperties))
            {
                if (productDetailParam.includeProperties.Contains("Product"))
                {
                    foreach (var productDetail in values)
                    {
                        productDetail.Product.ProductDetails = null;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(productDetailParam.Id))
            {
                values = values.Where(x => x.Id == productDetailParam.Id);
            }
            if (productDetailParam.ProductId != null)
            {
                values = values.Where(x => x.ProductId == productDetailParam.ProductId);
            }
            if (!string.IsNullOrWhiteSpace(productDetailParam.PackageId))
            {
                values = values.Where(x => x.PackageId == productDetailParam.PackageId);
            }
            if (!string.IsNullOrWhiteSpace(productDetailParam.ProductName))
            {
                values = values.Where(x => x.Product.Name.Contains(productDetailParam.ProductName, StringComparison.InvariantCultureIgnoreCase));
            }
            if (productDetailParam.CategoryId != null)
            {
                values = values.Where(x => x.Product.CategoryId == productDetailParam.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(productDetailParam.StatusId))
            {
                values = values.Where(x => x.StatusId == productDetailParam.StatusId);
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
                    case "category-id":
                        if (productDetailParam.dir == "asc")
                            values = values.OrderBy(d => d.Product.CategoryId);
                        else if (productDetailParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Product.CategoryId);
                        break;
                }
            }
            return values;
        }

        public async Task<PagedList<ProductDetail>> GetProductDetailWithPrice(ProductDetailParameter productDetailParam, PagingParameter paging)
        {
            var values = await _productDetailRepository.GetProductDetailWithPrice();
            foreach (var item in values)
            {
                item.Package.ProductDetails = null;
                item.Product.ProductDetails = null;
            }
            values = values.Where(x => x.StatusId == "3.1");
            foreach (var productDetail in values)
            {
                if(productDetail.PriceDetails != null)
                {
                    for (int i = 0; i < productDetail.PriceDetails.Count; i++)
                    {
                        productDetail.PriceDetails.ElementAt(i).Price = null;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(productDetailParam.includeProperties))
            {
                if (productDetailParam.includeProperties.Contains("Product"))
                {
                    foreach (var productDetail in values)
                    {
                        productDetail.Product.ProductDetails = null;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(productDetailParam.Id))
            {
                values = values.Where(x => x.Id == productDetailParam.Id);
            }

            if (productDetailParam.CategoryId != null)
            {
                values = values.Where(x => x.Product.CategoryId == productDetailParam.CategoryId);
            }
            if (productDetailParam.ProductId != null)
            {
                values = values.Where(x => x.ProductId == productDetailParam.ProductId);
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

        public PagedList<ProductDetail> GetStoreCart(ListIdString listId,
                                                                    ProductDetailParameter productDetailParam,
                                                                    PagingParameter paging)
        {
            var values = _productDetailRepository.GetProductDetailByListIdStoreManager(listId.ListId);
            foreach (var item in values)
            {
                item.Package.ProductDetails = null;
            }
            foreach (var productDetail in values)
            {
                if (productDetail.Product != null)
                {
                    productDetail.Product.ProductDetails = null;
                }
            }

            if (!string.IsNullOrWhiteSpace(productDetailParam.includeProperties))
            {
                if (productDetailParam.includeProperties.Contains("Product"))
                {
                    foreach (var productDetail in values)
                    {
                        productDetail.Product.ProductDetails = null;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(productDetailParam.Id))
            {
                values = values.Where(x => x.Id == productDetailParam.Id);
            }
            if (productDetailParam.ProductId != null)
            {
                values = values.Where(x => x.ProductId == productDetailParam.ProductId);
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
                }
            }

            return PagedList<ProductDetail>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }

        public PagedList<ProductDetail> GetProductDetailByListId(ListIdString listId,
                                                                    ProductDetailParameter productDetailParam,
                                                                    PagingParameter paging)
        {
            var values = _productDetailRepository.GetProductDetailByListId(listId.ListId);
            foreach (var item in values)
            {
                item.Package.ProductDetails = null;
            }
            foreach (var productDetail in values)
            {
                if(productDetail.Product != null)
                {
                    productDetail.Product.ProductDetails = null;
                }
                if (productDetail.PriceDetails != null)
                {
                    for (int i = 0; i < productDetail.PriceDetails.Count; i++)
                    {
                        productDetail.PriceDetails.ElementAt(i).Price = null;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(productDetailParam.includeProperties))
            {
                if (productDetailParam.includeProperties.Contains("Product"))
                {
                    foreach (var productDetail in values)
                    {
                        productDetail.Product.ProductDetails = null;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(productDetailParam.Id))
            {
                values = values.Where(x => x.Id == productDetailParam.Id);
            }
            if (productDetailParam.ProductId != null)
            {
                values = values.Where(x => x.ProductId == productDetailParam.ProductId);
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
                var values = _productDetailRepository.CheckProductDetaiilExistence(productDetail.ProductId,productDetail.PackageId, productDetail.Volume);
                if (values != null)
                {
                    if (values.StatusId == "3.2")
                    {
                        values.StatusId = "3.1";
                        _productDetailRepository.Update(values);
                        _productDetailRepository.SaveDbChange();
                        return true;
                    }
                }
                else
                {
                    _productDetailRepository.AddProductDetail(productDetail);
                    _productDetailRepository.SaveDbChange();
                    return true;

                }
                return false;
            }
            catch
            {
                return false;
            }
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
