using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class StoreRequestOrder
    {
        public StoreRequestOrder()
        {
            ProductStoreRequestDetails = new HashSet<ProductStoreRequestDetail>();
        }

        public StoreRequestOrder(string id, int storeRequestId, string? storeSupplyCode)
        {
            Id = id;
            StoreRequestId = storeRequestId;
            StoreSupplyCode = storeSupplyCode;
        }

        public string Id { get; set; } = null!;
        public int StoreRequestId { get; set; }
        public string? StoreSupplyCode { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string? Reason { get; set; }
        public string StatusId { get; set; } = null!;

        public virtual Status Status { get; set; } = null!;
        public virtual Store StoreRequest { get; set; } = null!;
        public virtual ICollection<ProductStoreRequestDetail> ProductStoreRequestDetails { get; set; }
    }
}
