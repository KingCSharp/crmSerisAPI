using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentAssignedWorkOrder
    {
        public int WorkOrderId { get; set; }
        public int EquipmentId { get; set; }
        public int CompanyId { get; set; }
        public string OrderNumber { get; set; }
        public int MachineHours { get; set; }
        public DateTime BillingDate { get; set; }
        public string RepairType { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
    }
}
