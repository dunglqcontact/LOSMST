using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using LOSMST.Models.Helper.InsertHelper;
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
            foreach (var item in values)
            {
                if (item.StoreRequest != null)
                {
                    item.StoreRequest.StoreRequestOrders = null;
                }
            }

            if (storeRequestOrderParam.includeProperties.Contains("StoreRequest"))
            {
                if (!string.IsNullOrWhiteSpace(storeRequestOrderParam.StoreRequestCode))
                {
                    values = values.Where(x => x.StoreRequest.Code == storeRequestOrderParam.StoreRequestCode);
                }
            }

            if (!string.IsNullOrWhiteSpace(storeRequestOrderParam.Id))
            {
                values = values.Where(x => x.Id == storeRequestOrderParam.Id);
            }


            if (!string.IsNullOrWhiteSpace(storeRequestOrderParam.StatusId))
            {
                values = values.Where(x => x.StatusId == storeRequestOrderParam.StatusId);
            }
            if (storeRequestOrderParam.StoreRequestId != null)
            {
                values = values.Where(x => x.StoreRequestId == storeRequestOrderParam.StoreRequestId);
            }
            if (storeRequestOrderParam.StoreSupplyCode != null)
            {
                values = values.Where(x => x.StoreSupplyCode == storeRequestOrderParam.StoreSupplyCode);
            }

            return PagedList<StoreRequestOrder>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }
        public bool InsertStoreRequestOrder(StoreRequestOrderInsertModel storeRequestOrder)
        {
            try
            {
                _storeRequestOrderRepository.InsertStoreRequestOrder(storeRequestOrder);
                _storeRequestOrderRepository.SaveDbChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CancelStoreRequestOrder(string id, string reason)
        {
            try
            {
                _storeRequestOrderRepository.CancelStoreRequestOrder(id, reason);
                _storeRequestOrderRepository.SaveDbChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
