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
    public class CustomerOrderDetailService
    {
        private readonly ICustomerOrderDetailRepository _customerOrderDetailRepository;

        public CustomerOrderDetailService(ICustomerOrderDetailRepository customerOrderDetailRepository)
        {
            _customerOrderDetailRepository = customerOrderDetailRepository;
        }
        public PagedList<CustomerOrderDetail> GetAllCustomerOrderDetail(CustomerOrderDetailParameter orderDetailParam, PagingParameter paging)
        {
            var values = _customerOrderDetailRepository.GetAllCustomerOrderDetail();
            foreach (var item in values)
            {
                if(item.ProductDetail != null)
                {
                    item.ProductDetail.CustomerOrderDetails = null;
                    item.ProductDetail.Package.ProductDetails = null;
                    if(item.ProductDetail.Product != null)
                    {
                        item.ProductDetail.Product.ProductDetails = null;
                    }
                }
            }
            if (orderDetailParam.Id != null)
            {
                values = values.Where(x => x.Id == orderDetailParam.Id);
            }

            if (!string.IsNullOrWhiteSpace(orderDetailParam.CustomerOrderId))
            {
                values = values.Where(x => x.CustomerOrderId == orderDetailParam.CustomerOrderId);
            }

            return PagedList<CustomerOrderDetail>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }
    }
}
