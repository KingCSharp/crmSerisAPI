using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesModelGroup
    {
        public int GroupId { get; set; }
        public int ModelId { get; set; }
        public string GroupName { get; set; }
        public string MandatoryFlag { get; set; }
        public string GroupCode { get; set; }
        public int SortOrder { get; set; }
        public bool Managed { get; set; }
        public bool Deleted { get; set; }
    }
}
