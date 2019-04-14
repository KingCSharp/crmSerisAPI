using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentAssignedWarranty
    {
        public int WarrantyId { get; set; }
        public int EquipmentId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string WarrantyType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StartSmu { get; set; }
        public int EndSmu { get; set; }
        public int Hours { get; set; }
        public bool Active { get; set; }
    }
}
