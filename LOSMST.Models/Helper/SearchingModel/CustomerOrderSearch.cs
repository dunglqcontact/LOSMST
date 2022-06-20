using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.SearchingModel
{
    public class CustomerOrderSearch
    {
        public string Id { get; set; } = null!;
        public double TotalPrice { get; set; }
        public int StoreId { get; set; }
        public int? StaffAccountId { get; set; }
        public int? ManagerAccountId { get; set; }
        public int CustomerAccountId { get; set; }
        public string StatusId { get; set; } = null!;
        public string? Reason { get; set; }
        public string? OrderDate { get; set; }
        public DateTime? EstimatedReceiveDate { get; set; }
        public DateTime? ReceiveDate { get; set; }

        public virtual Account CustomerAccount { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<CustomerOrderDetail> CustomerOrderDetails { get; set; }
    }
}
