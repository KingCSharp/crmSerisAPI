using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class BrokerDistributionList
    {
        public int DistributionId { get; set; }
        public string DistributionList { get; set; }
        public bool Deleted { get; set; }
    }
}
