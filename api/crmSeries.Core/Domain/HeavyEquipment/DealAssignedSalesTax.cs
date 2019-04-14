using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedSalesTax
    {
        public int AssignedId { get; set; }
        public int? DealId { get; set; }
        public string TaxType { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Value { get; set; }
        public bool? Excluded { get; set; }
        public string ExcludeReason { get; set; }
        public decimal? TaxableMax { get; set; }
        public bool? Editable { get; set; }
        public bool Deleted { get; set; }
    }
}
