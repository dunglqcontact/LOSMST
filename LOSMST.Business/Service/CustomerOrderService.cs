﻿using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
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
            if (customerParam.includeProperties != null)
            {
                if (customerParam.includeProperties.Contains("CustomerAccount"))
                {
                    if (!string.IsNullOrEmpty(customerParam.CustomerAccountName))
                    {
                        values = values.Where(x => x.CustomerAccount.Fullname != null && x.CustomerAccount.Fullname.Contains(customerParam.CustomerAccountName,StringComparison.InvariantCultureIgnoreCase));
                    }
                }
            }
            foreach (var item in values)
            {
                if (item.CustomerAccount != null)
                {
                    item.CustomerAccount.CustomerOrders = null;
                }
                if (item.Store != null)
                {
                    item.Store.CustomerOrders = null;
                }
            }
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
            if (customerParam.FromDate != null && customerParam.ToDate != null)
            {
                string fromDateStr = "0" + customerParam.FromDate.ToString();
                fromDateStr = fromDateStr.Substring(0, 10);
                if (fromDateStr.Substring(9) == " ")
                {
                    fromDateStr = fromDateStr.Substring(0, 3) + "0" + fromDateStr.Substring(3) + "00:00:00";
                }
                else
                {
                    fromDateStr += " 00:00:00";
                }

                string toDateStr = "0" + customerParam.ToDate.ToString();
                toDateStr = toDateStr.Substring(0, 10);
                if (toDateStr.Substring(9) == " ")
                {
                    toDateStr = toDateStr.Substring(0, 3) + "0" + toDateStr.Substring(3) + "23:59:59";
                }
                else
                {
                    toDateStr += " 23:59:59";
                }

                DateTime fromDate = DateTime.ParseExact(fromDateStr, "MM/dd/yyyy HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture);
                DateTime toDate = DateTime.ParseExact(toDateStr, "MM/dd/yyyy HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture);
                values = values
                    .Where(x => x.ReceiveDate >= fromDate && x.ReceiveDate <= toDate);
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
                    case "order-date":
                        if (customerParam.dir == "asc")
                            values = values.OrderBy(d => d.OrderDate);
                        else if (customerParam.dir == "desc")
                            values = values.OrderByDescending(d => d.OrderDate);
                        break;
                }
            }

            return PagedList<CustomerOrder>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }

        public CustomerOrder InsertCart(CustomerOrderInsertModel customerOrderInsert)
        {
            try
            {
                CustomerOrder customerOrder = _customerOrderRepository.InsertOrder(customerOrderInsert);
                _customerOrderRepository.SaveDbChange();
                return customerOrder;
            }catch (Exception ex)
            {
                return null;
            }
        }

        public bool CancelCustomerOrder(string id, string reason)
        {
            try
            {
                _customerOrderRepository.CancelCustomerOrder(id, reason);
                _customerOrderRepository.SaveDbChange();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
        public bool DenyCustomerOrder(string id, string reason)
        {
            try
            {
                _customerOrderRepository.DenyCustomerOrder(id, reason);
                _customerOrderRepository.SaveDbChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ApproveCustomerOrder(string id, DateTime? estimatedReceiveDate, int? managerAccountId)
        {
            try
            {
                _customerOrderRepository.ApproveCustomerOrder(id, estimatedReceiveDate, managerAccountId);
                _customerOrderRepository.SaveDbChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool FinishCustomerOrder(string customerOrderId, int staffAccountId)
        {
            try
            {
                _customerOrderRepository.FinishCustomerOrder(customerOrderId, staffAccountId);
                _customerOrderRepository.SaveDbChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
