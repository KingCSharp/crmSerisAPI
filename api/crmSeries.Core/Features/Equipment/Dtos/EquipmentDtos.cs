using System;

namespace crmSeries.Core.Features.Equipment.Dtos
{
    public class BaseEquipmentDto
    {
        public int ParentId { get; set; }

        public string ParentType { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public string Description { get; set; }

        public string SerialNumber { get; set; }

        public string StockNumber { get; set; }

        public string EquipmentNumber { get; set; }

        public string EquipmentYear { get; set; }

        public DateTime? StartDate { get; set; }

        public decimal StartHours { get; set; }

        public DateTime? LastSmrDate { get; set; }

        public decimal LastSmr { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal HoursPerDay { get; set; }

        public bool Inventory { get; set; }

        public string Fleet { get; set; }

        public string FleetType { get; set; }

        public string Status { get; set; }

        public string NewUsed { get; set; }

        public string GroupCode { get; set; }
    }

    public class GetEquipmentDto : BaseEquipmentDto
    {
        /// <summary>
        /// The equipment identifier.
        /// </summary>
        public int EquipmentId { get; set; }
    }
}
