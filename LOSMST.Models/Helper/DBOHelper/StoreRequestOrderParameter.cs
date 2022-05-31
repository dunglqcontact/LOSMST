using LOSMST.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOSMST.Models.Helper.DBOHelper
{
    public class StoreRequestOrderParameter
    {
        public string? Id { get; set; } = null!;
        public string? StoreRequestInvoiceCode { get; set; }
        public int? StoreRequestId { get; set; }
        public string? StoreSupplyCode { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string? Reason { get; set; }
        public string? StatusId { get; set; } = null!;

        public string? Status { get; set; } = null!;
        public string? StoreRequest { get; set; } = null!;
        public string? dir { get; set; } = "asc";
        public string? sort { get; set; } = null;
        public string? fields { get; set; } = null;
        public string? includeProperties { get; set; } = null;
    }
}
