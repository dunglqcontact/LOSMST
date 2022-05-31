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
    public class StoreProductDetailService
    {
        private readonly IStoreProductDetailRepository _storeProductDetailRepository;

        public StoreProductDetailService(IStoreProductDetailRepository storeProductDetailRepository)
        {
            _storeProductDetailRepository = storeProductDetailRepository;
        }

        public PagedList<StoreProductDetail> GetAllStoreProductDetail(StoreProductDetailParameter storeProductDetailParam, PagingParameter paging)
        {
            var values = _storeProductDetailRepository.GetAll(includeProperties: storeProductDetailParam.includeProperties);
            if(storeProductDetailParam.includeProperties != null)
            {
                if (storeProductDetailParam.includeProperties.Contains("ProductDetail"))
                {
                    foreach (var storeProductDetail in values)
                    {
                        storeProductDetail.ProductDetail.StoreProductDetails = null;
                    }
                }
                if (storeProductDetailParam.includeProperties.Contains("Store"))
                {
                    foreach(var storeProductDetail in values)
                    {
                        storeProductDetail.Store.StoreProductDetails = null;
                    }
                }
                
            }
            if (storeProductDetailParam.Id != null)
            {
                values = values.Where(x => x.Id == storeProductDetailParam.Id);
            }
            if (storeProductDetailParam.StoreId != null)
            {
                values = values.Where(x => x.StoreId == storeProductDetailParam.StoreId);
            }
            if (!string.IsNullOrWhiteSpace(storeProductDetailParam.ProductDetailId)){
                values = values.Where(x => x.ProductDetailId == storeProductDetailParam.ProductDetailId); 
            }
            if (!string.IsNullOrWhiteSpace(storeProductDetailParam.sort))
            {
                switch (storeProductDetailParam.sort)
                {
                    case "id":
                        if(storeProductDetailParam.dir == "asc")
                            values = values.OrderBy(x => x.Id);
                        else if(storeProductDetailParam.dir == "desc")
                            values.OrderByDescending(x => x.Id);
                        break;
                    case "productDetailId":
                        if(storeProductDetailParam.dir == "asc")
                            values = values.OrderBy(values => values.ProductDetailId);
                        else if(storeProductDetailParam.dir=="desc")
                            values = values.OrderByDescending(values => values.ProductDetailId);
                        break;
                    case "storeId":
                        if (storeProductDetailParam.dir == "asc")
                            values = values.OrderBy(values => values.StoreId);
                        else if (storeProductDetailParam.dir == "desc")
                            values = values.OrderByDescending(values => values.StoreId);
                        break;
                }
            }

            return PagedList<StoreProductDetail>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }
        public PagedList<StoreProductDetail> GetStoreInventory(StoreProductDetailParameter storeProductDetailParam, PagingParameter paging)
        {
            var values = _storeProductDetailRepository.GetStoreProductDetail();
            if (storeProductDetailParam.Id != null)
            {
                values = values.Where(x => x.Id == storeProductDetailParam.Id);
            }
            if (storeProductDetailParam.StoreId != null)
            {
                values = values.Where(x => x.StoreId == storeProductDetailParam.StoreId);
            }
            if (!string.IsNullOrWhiteSpace(storeProductDetailParam.ProductDetailId))
            {
                values = values.Where(x => x.ProductDetailId == storeProductDetailParam.ProductDetailId);
            }
            if (!string.IsNullOrWhiteSpace(storeProductDetailParam.ProductDetailId))
            {
                values = values.Where(x => x.ProductDetailId == storeProductDetailParam.ProductDetailId);
            }
            if (storeProductDetailParam.CategoryId != null)
            {
                values = values.Where(x => x.ProductDetail.Product.CategoryId == storeProductDetailParam.CategoryId);
            }
            foreach (var storeProductDetail in values)
            {
                if (storeProductDetail.ProductDetail != null)
                {
                    storeProductDetail.ProductDetail.StoreProductDetails = null;
                    storeProductDetail.ProductDetail.Product.ProductDetails = null;
                }
            }
            if (!string.IsNullOrWhiteSpace(storeProductDetailParam.sort))
            {
                switch (storeProductDetailParam.sort)
                {
                    case "id":
                        if (storeProductDetailParam.dir == "asc")
                            values = values.OrderBy(x => x.Id);
                        else if (storeProductDetailParam.dir == "desc")
                            values.OrderByDescending(x => x.Id);
                        break;
                    case "productDetailId":
                        if (storeProductDetailParam.dir == "asc")
                            values = values.OrderBy(values => values.ProductDetailId);
                        else if (storeProductDetailParam.dir == "desc")
                            values = values.OrderByDescending(values => values.ProductDetailId);
                        break;
                    case "storeId":
                        if (storeProductDetailParam.dir == "asc")
                            values = values.OrderBy(values => values.StoreId);
                        else if (storeProductDetailParam.dir == "desc")
                            values = values.OrderByDescending(values => values.StoreId);
                        break;
                }
            }

            return PagedList<StoreProductDetail>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }
    }
}
