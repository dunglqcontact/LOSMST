using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.DBOHelper
{
    public class StoreParameter
    {
        public int? Id { get; set; }
        public string? Code { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? StoreCategoryId { get; set; } = null;
        public string? District { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Phone { get; set; } = null;
        public string? StatusId { get; set; } = null;
        public string? Status { get; set; } = null;
        public string? StoreCategory { get; set; } = null;
        public string? dir { get; set; } = "asc";
        public string? sort { get; set; } = null;
        public string? fields { get; set; } = null;
        public string? includeProperties { get; set; } = null;
    }
}
