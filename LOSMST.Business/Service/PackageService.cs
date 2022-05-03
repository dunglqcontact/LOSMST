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
    public class PackageService
    {
        private readonly IPackageRepository _packageRepository;

        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public IEnumerable GetRSMSPackage()
        {
            var packages = _packageRepository.GetAll().Where(x => x.Id == "U03" || x.Id == "U05");

            return packages;
        }

        public PagedList<Package> GetAllPackages(PackageParameter packageParam, PagingParameter paging)
        {
            var values = _packageRepository.GetAll(includeProperties: packageParam.includeProperties);

            if (!string.IsNullOrWhiteSpace(packageParam.Id))
            {
                values = values.Where(x => x.Id == packageParam.Id);
            }
            if (!string.IsNullOrWhiteSpace(packageParam.Name))
            {
                values = values.Where(x => x.Name.Contains(packageParam.Name, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(packageParam.sort))
            {
                switch (packageParam.sort)
                {
                    case "Id":
                        if (packageParam.dir == "asc")
                            values = values.OrderBy(d => d.Id);
                        else if (packageParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Id);
                        break;
                    case "Name":
                        if (packageParam.dir == "asc")
                            values = values.OrderBy(d => d.Name);
                        else if (packageParam.dir == "desc")
                            values = values.OrderByDescending(d => d.Name);
                        break;
                }
            }

            return PagedList<Package>.ToPagedList(values.AsQueryable(),
                paging.PageNumber,
                paging.PageSize);
        }
    }
}
