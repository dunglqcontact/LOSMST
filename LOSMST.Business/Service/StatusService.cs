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
    public class StatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public IEnumerable<Status> GetAllStatus(StatusParameter statusParam, PagingParameter paging)
        {
            var values = _statusRepository.GetAll(includeProperties: statusParam.includeProperties);
            if(!string.IsNullOrWhiteSpace(statusParam.Id))
            {
                values = values.Where(x => x.Id == statusParam.Id);
            }
            if (!string.IsNullOrWhiteSpace(statusParam.Name)){
                values = values.Where(x => x.Name.Contains(statusParam.Name, StringComparison.InvariantCultureIgnoreCase)); 
            }
            if (!string.IsNullOrWhiteSpace(statusParam.sort))
            {
                switch (statusParam.sort)
                {
                    case "Id":
                        if(statusParam.dir == "asc")
                            values = values.OrderBy(x => x.Id);
                        else if(statusParam.dir == "desc")
                            values.OrderByDescending(x => x.Id);
                        break;
                    case "Name":
                        if(statusParam.dir == "asc")
                            values = values.OrderBy(values => values.Name);
                        else if(statusParam.dir=="desc")
                            values = values.OrderByDescending(values => values.Name);
                        break;
                }
            }

            return PagedList<Status>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }
    }
}
