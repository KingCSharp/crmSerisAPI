using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanyAssignedRevenue
    {
        public int RevenueId { get; set; }
        public int CompanyId { get; set; }
        public string RevenueType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Revenue { get; set; }
    }
}
