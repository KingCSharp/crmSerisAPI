using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanyAssignedAr
    {
        public int AssignedId { get; set; }
        public int CompanyId { get; set; }
        public decimal CurrentMonth { get; set; }
        public decimal Month2 { get; set; }
        public decimal Month3 { get; set; }
        public decimal Month4 { get; set; }
        public decimal AllOther { get; set; }
    }
}
