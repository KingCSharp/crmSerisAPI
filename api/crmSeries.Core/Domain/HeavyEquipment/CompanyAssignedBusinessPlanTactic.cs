using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanyAssignedBusinessPlanTactic
    {
        public int AssignedId { get; set; }
        public int AssignedPlanId { get; set; }
        public int TacticId { get; set; }
        public DateTime TargetDate { get; set; }
        public bool Complete { get; set; }
        public DateTime? CompleteDate { get; set; }
        public bool Deleted { get; set; }
    }
}
