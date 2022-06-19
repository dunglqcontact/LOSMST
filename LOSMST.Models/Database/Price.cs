using System;
using System.Collections.Generic;

namespace LOSMST.Models.Database
{
    public partial class Price
    {
        public Price()
        {
            PriceDetails = new HashSet<PriceDetail>();
        }

        public string Id { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StatusId { get; set; } = null!;

        public virtual Status Status { get; set; } = null!;
        public virtual ICollection<PriceDetail> PriceDetails { get; set; }
    }
}
