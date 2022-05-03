using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LOSMST.Models.Database
{
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            CustomerOrderDetails = new HashSet<CustomerOrderDetail>();
            ExportInventoryDetails = new HashSet<ExportInventoryDetail>();
            ImportInventoryDetails = new HashSet<ImportInventoryDetail>();
            PriceDetails = new HashSet<PriceDetail>();
            ProductStoreRequestDetails = new HashSet<ProductStoreRequestDetail>();
        }

        public string Id { get; set; } = null!;
        public int ProductId { get; set; }
        public double Volume { get; set; }
        public string? PackageId { get; set; } = null!;
        public string? StatusId { get; set; } = null!;
        public double QuantityWholeSalePrice { get; set; }
        public virtual Package? Package { get; set; } = null!;
        public virtual Product? Product { get; set; } = null!;
        public virtual Status? Status { get; set; } = null!;
        public virtual ICollection<CustomerOrderDetail> CustomerOrderDetails { get; set; }
        public virtual ICollection<ExportInventoryDetail> ExportInventoryDetails { get; set; }
        public virtual ICollection<ImportInventoryDetail> ImportInventoryDetails { get; set; }
        public virtual ICollection<PriceDetail> PriceDetails { get; set; }
        public virtual ICollection<ProductStoreRequestDetail> ProductStoreRequestDetails { get; set; }
    }
}
