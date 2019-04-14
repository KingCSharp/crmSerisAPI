using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class BrokerAssignedDistribution
    {
        public int AssignedId { get; set; }
        public int DistributionId { get; set; }
        public int BrokerId { get; set; }
    }
}
