using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedWorkOrder
    {
        public int WorkOrderId { get; set; }
        public int DealId { get; set; }
        public int DbworkOrderId { get; set; }
        public string JobCode { get; set; }
        public string Description { get; set; }
        public string OutputDisplay { get; set; }
        public decimal Cost { get; set; }
        public string QuoteNo { get; set; }
        public bool CustomMargin { get; set; }
        public decimal Margin { get; set; }
        public bool Optional { get; set; }
        public bool Deleted { get; set; }
        public bool Editable { get; set; }
    }
}
