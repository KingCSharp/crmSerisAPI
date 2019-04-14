using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class OpportunitySaleType
    {
        public int SaleTypeId { get; set; }
        public string SaleType { get; set; }
        public bool Deleted { get; set; }
    }
}
