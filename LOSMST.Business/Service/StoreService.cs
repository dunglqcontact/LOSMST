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
    public class StoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public PagedList<Store> GetAllStores(StoreParameter storeParam, PagingParameter paging)
        {
            var values = _storeRepository.GetAll(includeProperties: storeParam.includeProperties);

            if (storeParam.Id != null)
            {
                values = values.Where(x => x.Id == storeParam.Id);
            }
            if (!string.IsNullOrWhiteSpace(storeParam.Name))
            {
                values = values.Where(x => x.Name.Contains(storeParam.Name, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(storeParam.StoreCategoryId))
            {
                values = values.Where(x => x.StoreCategoryId == storeParam.StoreCategoryId);
            }
            if (!string.IsNullOrWhiteSpace(storeParam.District))
            {
                values = values.Where(x => x.District == storeParam.District);
            }
            if (!string.IsNullOrWhiteSpace(storeParam.Email))
            {
                values = values.Where(x => x.Email.Contains(storeParam.Email, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(storeParam.Phone))
            {
                values = values.Where(x => x.Phone.Contains(storeParam.Phone));
            }
            if (!string.IsNullOrWhiteSpace(storeParam.StatusId))
            {
                values = values.Where(x => x.StatusId == storeParam.StatusId);
            }
            if (!string.IsNullOrWhiteSpace(storeParam.sort))
            {
                switch (storeParam.sort)
                {
                    case "Id":
                        if (storeParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (storeParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                    case "Name":
                        if (storeParam.dir == "asc")
                            values = values.OrderBy(d => d.Name);
                        else if (storeParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Name);
                        break;
                    case "Code":
                        if (storeParam.dir == "asc")
                            values = values.OrderBy(d => d.Code);
                        else if (storeParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Code);
                        break;
                    case "StoreCategoryId":
                        if (storeParam.dir == "asc")
                            values = values.OrderBy(d => d.StoreCategoryId);
                        else if (storeParam.dir == "desc")
                            values = values.OrderByDescending(d => d.StoreCategoryId);
                        break;
                }
            }

            return PagedList<Store>.ToPagedList(values.AsQueryable(),
            paging.PageNumber,
            paging.PageSize);
        }
        public bool Add(Store store)
        {
            try
            {
                var abc = store;
                _storeRepository.Add(store);

                _storeRepository.SaveDbChange();
                return true;
            }
            catch { return false; }
        }

        public bool Update(Store store)
        {
            try
            {
                _storeRepository.Update(store);
                _storeRepository.SaveDbChange();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
