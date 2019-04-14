using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class Pricing
    {
        public int PricingId { get; set; }
        public string ChargeType { get; set; }
        public string Charge { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
