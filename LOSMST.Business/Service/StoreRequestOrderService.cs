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
            var values = _storeRequestOrderRepository.GetAllStoreRequestOrder(includeProperties: storeRequestOrderParam.includeProperties);
            foreach (var item in values)
            {
                if (item.StoreRequest != null)
                {
                    item.StoreRequest.StoreRequestOrders = null;
                }
                if (item.ProductStoreRequestDetails != null) {
                    for (int i = 0; i < item.ProductStoreRequestDetails.Count; i++)
                    {
                        item.ProductStoreRequestDetails.ElementAt(i).ProductDetail.ProductStoreRequestDetails = null;
                    }
                }
            }
            if (!string.IsNullOrEmpty(storeRequestOrderParam.includeProperties))
            {
                if (storeRequestOrderParam.includeProperties.Contains("StoreRequest"))
                {
                    if (!string.IsNullOrWhiteSpace(storeRequestOrderParam.StoreRequestCode))
                    {
                        values = values.Where(x => x.StoreRequest.Code == storeRequestOrderParam.StoreRequestCode);
                    }
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

        public bool ApproveStoreRequestOrder(StoreRequestOrder storeRequestOrder)
        {
            try
            {
                _storeRequestOrderRepository.ApproveStoreRequestOrder(storeRequestOrder);
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
        public bool DenyStoreRequestOrder(string id, string reason)
        {
            try
            {
                _storeRequestOrderRepository.DenyStoreRequestOrder(id, reason);
                _storeRequestOrderRepository.SaveDbChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool FinishOrder(string orderId)
        {
            try
            {
                if (_storeRequestOrderRepository.FinishStoreRequestOrder(orderId))
                {
                    _storeRequestOrderRepository.SaveDbChange();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
