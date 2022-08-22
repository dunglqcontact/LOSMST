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

        public StoreRequestOrder(DateTime requestDate, string id, int storeRequestId, string? storeSupplyCode, ICollection<ProductStoreRequestDetail> productStoreRequestDetails)
        {
            Id = id;
            StoreRequestId = storeRequestId;
            StoreSupplyCode = storeSupplyCode;
            ProductStoreRequestDetails = productStoreRequestDetails;
            RequestDate = requestDate;
        }

        public string Id { get; set; } = null!;
        public int StoreRequestId { get; set; }
        public string? StoreSupplyCode { get; set; } = null!;
        public DateTime? RequestDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string? Reason { get; set; }
        public DateTime? EstimatedReceiveDate { get; set; }
        public string? StatusId { get; set; } = null!;

        public virtual Status? Status { get; set; } = null!;
        public virtual Store? StoreRequest { get; set; } = null!;
        public virtual ICollection<ProductStoreRequestDetail> ProductStoreRequestDetails { get; set; }
    }
}
