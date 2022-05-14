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
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<Role> GetStoreRole(int storeId)
        {
            var roles = _roleRepository.GetStoreRole(storeId);

            return roles;
        }

        public PagedList<Role> GetAllRoles(RoleParameter roleParam, PagingParameter paging)
        {
            var values = _roleRepository.GetAll(includeProperties: roleParam.includeProperties);
            if (roleParam.notIncludeId != null)
            {
                foreach (var notInclude in roleParam.notIncludeId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    values = values.Where(x => x.Id != notInclude);
                }
            }
            if (!string.IsNullOrWhiteSpace(roleParam.notIncludeId))
            {
                values = values.Where(x => x.Id != roleParam.notIncludeId);
            }

            if (!string.IsNullOrWhiteSpace(roleParam.Id))
            {
                values = values.Where(x => x.Id == roleParam.Id);
            }
            if (!string.IsNullOrWhiteSpace(roleParam.Name))
            {
                values = values.Where(x => x.Name.Contains(roleParam.Name, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(roleParam.sort))
            {
                switch (roleParam.sort)
                {
                    case "Id":
                        if (roleParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (roleParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                    case "Name":
                        if (roleParam.dir == "asc")
                            values = values.OrderBy(d => d.Name);
                        else if (roleParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Name);
                        break;
                }
            }

            return PagedList<Role>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }
    }
}
