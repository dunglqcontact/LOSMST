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
    public class StoreRequestOrderService
    {
        private readonly IStoreRequestOrderRepository _storeRequestOrderRepository;

        public StoreRequestOrderService(IStoreRequestOrderRepository storeRequestOrderRepository)
        {
            _storeRequestOrderRepository = storeRequestOrderRepository;
        }

        public PagedList<StoreRequestOrder> GetAllStoreRequestOrder(StoreRequestOrderParameter storeRequestOrderParam, PagingParameter paging)
        {
            var values = _storeRequestOrderRepository.GetAll(includeProperties: storeRequestOrderParam.includeProperties);
            if (storeRequestOrderParam.Id != null)
            {
                values = values.Where(x => x.Id == storeRequestOrderParam.Id);
            }
           
            if (!string.IsNullOrWhiteSpace(storeRequestOrderParam.sort))
            {
                switch (storeRequestOrderParam.sort)
                {
                    case "id":
                        if(storeRequestOrderParam.dir == "asc")
                            values = values.OrderBy(x => x.Id);
                        else if(storeRequestOrderParam.dir == "desc")
                            values.OrderByDescending(x => x.Id);
                        break;
/*                    case "productDetailId":
                        if(storeRequestOrderParam.dir == "asc")
                            values = values.OrderBy(values => values.ProductDetailId);
                        else if(storeRequestOrderParam.dir=="desc")
                            values = values.OrderByDescending(values => values.ProductDetailId);
                        break;
                    case "storeId":
                        if (storeRequestOrderParam.dir == "asc")
                            values = values.OrderBy(values => values.StoreId);
                        else if (storeRequestOrderParam.dir == "desc")
                            values = values.OrderByDescending(values => values.StoreId);
                        break;*/
                }
            }

            return PagedList<StoreRequestOrder>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }
    }
}
