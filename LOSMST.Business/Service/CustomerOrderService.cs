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
    public class CustomerOrderService
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;

        public CustomerOrderService(ICustomerOrderRepository customerOrderRepository)
        {
            _customerOrderRepository = customerOrderRepository;
        }
        public PagedList<CustomerOrder> GetAllCustomerOrder(CustomerOrderParameter customerParam, PagingParameter paging)
        {
            var values = _customerOrderRepository.GetAll(includeProperties: customerParam.includeProperties);
            if (!string.IsNullOrWhiteSpace(customerParam.Id))
            {
                values = values.Where(x => x.Id == customerParam.Id);
            }
            if(customerParam.CustomerAccountId != null)
            {
                values = values.Where(x => x.CustomerAccountId == customerParam.CustomerAccountId);
            }
            if (customerParam.StoreId != null)
            {
                values = values.Where(x => x.StoreId == customerParam.StoreId);
            }
            if (!string.IsNullOrWhiteSpace(customerParam.StatusId))
            {
                if (customerParam.StatusId.Equals("deny"))
                {
                    values = values.Where(x => x.StatusId == "2.4" || x.StatusId == "2.5");
                }
                else
                {
                    values = values.Where(x => x.StatusId == customerParam.StatusId);
                }
            }

            if (!string.IsNullOrWhiteSpace(customerParam.sort))
            {
                switch (customerParam.sort)
                {
                    case "id":
                        if (customerParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (customerParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                }
            }

            return PagedList<CustomerOrder>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }

        public bool InsertCart(CustomerOrderInsertModel customerOrder)
        {
            try
            {
                _customerOrderRepository.InsertOrder(customerOrder);
                _customerOrderRepository.SaveDbChange();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
