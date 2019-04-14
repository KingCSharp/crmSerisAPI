using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class BusinessPlanType
    {
        public int TypeId { get; set; }
        public string PlanType { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public bool MultiPlan { get; set; }
        public bool Deleted { get; set; }
    }
}
