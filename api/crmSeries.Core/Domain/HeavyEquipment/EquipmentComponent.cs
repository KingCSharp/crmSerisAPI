using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentComponent
    {
        public EquipmentComponent()
        {
            EquipmentComponentAssignedRating = new HashSet<EquipmentComponentAssignedRating>();
        }

        public int ComponentId { get; set; }
        public int EquipmentId { get; set; }
        public int ComponentTypeId { get; set; }
        public int ComponentLookupId { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal StartHours { get; set; }
        public string PartNo { get; set; }
        public string SerialNo { get; set; }
        public decimal AverageLife { get; set; }
        public decimal AdjustedLife { get; set; }
        public decimal RemainingLife { get; set; }
        public decimal PercentUsed { get; set; }
        public DateTime? DueDate { get; set; }
        public string Comments { get; set; }
        public bool Replaced { get; set; }
        public bool Deleted { get; set; }

        public Equipment Equipment { get; set; }
        public ICollection<EquipmentComponentAssignedRating> EquipmentComponentAssignedRating { get; set; }
    }
}
