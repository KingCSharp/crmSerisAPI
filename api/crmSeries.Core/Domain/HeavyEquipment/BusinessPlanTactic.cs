using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class BusinessPlanTactic
    {
        public int TacticId { get; set; }
        public int BusinessPlanId { get; set; }
        public int Sequence { get; set; }
        public string Activity { get; set; }
        public string Purpose { get; set; }
        public int DaysAfterPlanStart { get; set; }
        public bool Recurring { get; set; }
        public int RecurringCount { get; set; }
        public int RecurringInterval { get; set; }
        public bool PreCallPlan { get; set; }
        public int PreCallDaysBeforeTactic { get; set; }
        public bool Deleted { get; set; }
    }
}
