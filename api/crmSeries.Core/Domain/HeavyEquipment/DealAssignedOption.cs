using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedOption
    {
        public int OptionId { get; set; }
        public int DealId { get; set; }
        public int DboptionId { get; set; }
        public string SalesCode { get; set; }
        public string Description { get; set; }
        public string OutputDisplay { get; set; }
        public decimal List { get; set; }
        public bool BaseMachine { get; set; }
        public bool Optional { get; set; }
        public bool? UseExchange { get; set; }
        public bool ExcludeDiscount { get; set; }
        public bool Deleted { get; set; }
    }
}
