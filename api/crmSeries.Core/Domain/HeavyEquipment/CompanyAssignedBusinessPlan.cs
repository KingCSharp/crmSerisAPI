using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanyAssignedBusinessPlan
    {
        public int AssignedId { get; set; }
        public int CompanyId { get; set; }
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public decimal RevenueGoal { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanEndDate { get; set; }
        public bool Deleted { get; set; }
    }
}
