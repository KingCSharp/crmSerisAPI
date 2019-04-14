using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesModelItem
    {
        public int ItemId { get; set; }
        public int GroupId { get; set; }
        public string SalesCode { get; set; }
        public int ControlGroupId { get; set; }
        public int ControlItemId { get; set; }
        public int SortOrder { get; set; }
        public string Description { get; set; }
        public string ExtendedDescription { get; set; }
        public decimal ListPrice { get; set; }
        public bool BaseMachine { get; set; }
        public bool ExcludeDiscounts { get; set; }
        public bool Managed { get; set; }
        public string Notes { get; set; }
        public int LeadTime { get; set; }
        public bool Deleted { get; set; }
    }
}
