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
    public class StoreCategoryService
    {
        private readonly IStoreCategoryRepository _storeCategoryRepository;

        public StoreCategoryService(IStoreCategoryRepository storeCategoryRepository)
        {
            _storeCategoryRepository = storeCategoryRepository;
        }

        public IEnumerable GetRSMSStoreCategory()
        {
            var storeCategorys = _storeCategoryRepository.GetAll().Where(x => x.Id == "U03" || x.Id == "U05");

            return storeCategorys;
        }

        public PagedList<StoreCategory> GetAllStoreCategories(StoreCategoryParameter storeCategoryParam, PagingParameter paging)
        {
            var values = _storeCategoryRepository.GetAll(includeProperties: storeCategoryParam.includeProperties);

            if (!string.IsNullOrWhiteSpace(storeCategoryParam.Id))
            {
                values = values.Where(x => x.Id == storeCategoryParam.Id);
            }
            if (!string.IsNullOrWhiteSpace(storeCategoryParam.Name))
            {
                values = values.Where(x => x.Name.Contains(storeCategoryParam.Name, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(storeCategoryParam.sort))
            {
                switch (storeCategoryParam.sort)
                {
                    case "Id":
                        if (storeCategoryParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (storeCategoryParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                    case "Name":
                        if (storeCategoryParam.dir == "asc")
                            values = values.OrderBy(d => d.Name);
                        else if (storeCategoryParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Name);
                        break;
                }
            }

            return PagedList<StoreCategory>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }
    }
}
