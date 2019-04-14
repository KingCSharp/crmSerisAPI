using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Equipment
    {
        public Equipment()
        {
            EquipmentComponent = new HashSet<EquipmentComponent>();
        }

        public int EquipmentId { get; set; }
        public int ParentId { get; set; }
        public string ParentType { get; set; }
        public bool? Machine { get; set; }
        public int CategoryId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string SerialNo { get; set; }
        public string StockNo { get; set; }
        public string EquipmentNo { get; set; }
        public string EquipYear { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal StartHours { get; set; }
        public DateTime? LastSmrdate { get; set; }
        public decimal LastSmr { get; set; }
        public bool? Active { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal HoursPerDay { get; set; }
        public int HoursPerDayRange { get; set; }
        public int UcwearProfileId { get; set; }
        public DateTime? UcstartDate { get; set; }
        public decimal UcstartHours { get; set; }
        public int PmprofileId { get; set; }
        public DateTime? PmstartDate { get; set; }
        public decimal PmstartHours { get; set; }
        public bool Pmactive { get; set; }
        public int PminitialInterval { get; set; }
        public int PmintervalStep { get; set; }
        public int PmmaxHours { get; set; }
        public int ComponentProfileId { get; set; }
        public int GetprofileId { get; set; }
        public bool Inventory { get; set; }
        public int CurrentBranchId { get; set; }
        public string Fleet { get; set; }
        public string FleetType { get; set; }
        public string Status { get; set; }
        public string NewUsed { get; set; }
        public string GroupCode { get; set; }
        public decimal AcquisitionCost { get; set; }
        public decimal BookValue { get; set; }
        public decimal CurrentCost { get; set; }
        public decimal ListPrice { get; set; }
        public decimal SellPrice { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public bool? AvailableForQuote { get; set; }
        public string InventoryNotes { get; set; }
        public string InventorySpecs { get; set; }
        public bool Deleted { get; set; }
        public string Class { get; set; }
        public string InOut { get; set; }

        public ICollection<EquipmentComponent> EquipmentComponent { get; set; }
    }
}
