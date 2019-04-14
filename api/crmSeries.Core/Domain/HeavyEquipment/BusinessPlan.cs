using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class BusinessPlan
    {
        public int BusinessPlanId { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public int RoleId { get; set; }
        public string BusinessPlan1 { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool Template { get; set; }
        public bool Locked { get; set; }
        public bool Rollover { get; set; }
        public bool Deleted { get; set; }
    }
}
