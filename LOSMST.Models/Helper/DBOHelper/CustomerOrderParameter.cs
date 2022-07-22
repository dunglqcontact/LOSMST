using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.DBOHelper
{
    public class CustomerOrderParameter
    {
        public string? Id { get; set; } = null!;
        public double? TotalPrice { get; set; }
        public int? StoreId { get; set; }
        public int? StaffAccountId { get; set; }
        public int? ManagerAccountId { get; set; }
        public int? CustomerAccountId { get; set; }
        public string? CustomerAccountName { get; set; }
        public string? StatusId { get; set; } = null!;
        public string? Reason { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? EstimatedReceiveDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string? dir { get; set; } = "asc";
        public string? sort { get; set; } = null;
        public string? fields { get; set; } = null;
        public string? includeProperties { get; set; } = null;
    }
}
