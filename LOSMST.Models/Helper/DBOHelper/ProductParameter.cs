using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.DBOHelper
{
    public class ProductParameter
    {
        public int? Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Image { get; set; } = null;
        public int? CategoryId { get; set; }
        public string? QualityLevelFeature { get; set; } = null;
        public string? Brief { get; set; } = null;
        public string? GeneralBenefit { get; set; } = null;
        public string? Apply { get; set; } = null;
        public string? Preserve { get; set; } = null;
        public string? StatusId { get; set; } = null;

        public string? Category { get; set; } = null;
        public string? Status { get; set; } = null;
        public string? dir { get; set; } = "asc";
        public string? sort { get; set; } = null;
        public string? fields { get; set; } = null;
        public string? includeProperties { get; set; } = null;
    }
}
