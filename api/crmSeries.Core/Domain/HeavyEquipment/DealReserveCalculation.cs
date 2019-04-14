using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealReserveCalculation
    {
        public int ReserveId { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public decimal Amount { get; set; }
        public bool Editable { get; set; }
        public bool IncludeMachine { get; set; }
        public bool IncludeAttachments { get; set; }
        public bool IncludeWarranties { get; set; }
        public bool IncludeWorkOrders { get; set; }
        public string WorkOrderFilter { get; set; }
        public bool IncludeFreight { get; set; }
        public bool IncludeNonStandardDiscounts { get; set; }
        public bool IncludeNationalAccounts { get; set; }
    }
}
