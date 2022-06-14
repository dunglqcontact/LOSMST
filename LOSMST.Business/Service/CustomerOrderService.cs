using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
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
