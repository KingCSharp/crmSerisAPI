using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesSpec
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int LineNumber { get; set; }
        public string LeftCol { get; set; }
        public bool LeftBold { get; set; }
        public string RightCol { get; set; }
        public bool RightBold { get; set; }
    }
}
