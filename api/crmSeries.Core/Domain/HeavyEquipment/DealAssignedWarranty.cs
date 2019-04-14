using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedWarranty
    {
        public int WarrantyId { get; set; }
        public int DealId { get; set; }
        public int DbwarrantyId { get; set; }
        public int Hours { get; set; }
        public int Months { get; set; }
        public string Description { get; set; }
        public string OutputDisplay { get; set; }
        public decimal Cost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StartSmr { get; set; }
        public int EndSmr { get; set; }
        public bool Optional { get; set; }
        public bool CustomMargin { get; set; }
        public decimal Margin { get; set; }
        public bool Deleted { get; set; }
    }
}
