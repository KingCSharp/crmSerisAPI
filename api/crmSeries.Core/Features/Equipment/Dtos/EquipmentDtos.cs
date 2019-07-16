using System;

namespace crmSeries.Core.Features.Equipment.Dtos
{
    public class BaseEquipmentDto
    {
        /// <summary>
        /// The identifier of the category the equipment is classified as.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// The unique identifier of the parent of this equipment.  See
        /// ParentType to determine the parent type.
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// The type of the parent of this equipment. 
        /// </summary>
        public string ParentType { get; set; }

        /// <summary>
        /// The product name of the equipment.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The brand of the equipment.
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// A description or classification the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The serial number of the equipment.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// The stock number of the equipment, typically
        /// assigned by dealer.
        /// </summary>
        public string StockNumber { get; set; }

        /// <summary>
        /// The equipment number of the equipment, typically
        /// assigned by the owner of the equipment.
        /// </summary>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// The year the equipment was manufactured in.
        /// </summary>
        public string EquipmentYear { get; set; }

        /// <summary>
        /// The date the equipment started getting used in production.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The number of hours the equipment has since it began use.
        /// </summary>
        public decimal StartHours { get; set; }

        /// <summary>
        /// Represents the date of the last system meter reading.
        /// </summary>
        public DateTime? LastSmrDate { get; set; }

        /// <summary>
        /// Represents the system meter reading.  Smr is represented
        /// in either hours of miles, typically hours.
        /// </summary>
        public decimal LastSmr { get; set; }

        /// <summary>
        /// The current latitude of the equipment. 
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// The current longitude of the equipment.
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Average hours per day over the a past interval.
        /// </summary>
        public decimal HoursPerDay { get; set; }

        /// <summary>
        /// Does it belong to the dealership?  True = belongs to dealer.
        /// </summary>
        public bool Inventory { get; set; }

        /// <summary>
        /// The fleet the equipment is part of.
        /// </summary>
        public string Fleet { get; set; }

        /// <summary>
        /// The equipment's fleet type.
        /// </summary>
        public string FleetType { get; set; }

        /// <summary>
        /// The status of the equipment.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Is the equipment new or used.
        /// </summary>
        public string NewUsed { get; set; }

        /// <summary>
        /// The equipment's group code.  
        /// </summary>
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
