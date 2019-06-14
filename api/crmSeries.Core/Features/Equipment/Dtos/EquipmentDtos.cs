using System;

namespace crmSeries.Core.Features.Equipment
{
    public class BaseEquipmentDto
    {
        /// <summary>
        /// The id of the parent entity for this equipment.
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// The table / record type that the ParentId links to.
        /// </summary>
        public string ParentType { get; set; }

        /// <summary>
        /// Denotes if this equipment is a machine.
        /// </summary>
        public bool? Machine { get; set; }

        /// <summary>
        /// The make for this equipment.
        /// </summary>
        public string Make { get; set; }
        
        /// <summary>
        /// The model for this equipment.
        /// </summary>
        public string Model { get; set; }
        
        /// <summary>
        /// The stock number for this equipment.
        /// </summary>
        public string StockNo { get; set; }

        /// <summary>
        /// The serial number for this equipment.
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// A description for this equipment.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The equipment year for this equipment.
        /// </summary>
        public string EquipYear { get; set; }

        /// <summary>
        /// The fleet this equipment belongs to.
        /// </summary>
        public string Fleet { get; set; }

        /// <summary>
        /// The type for the fleet this equipment belongs to.
        /// </summary>
        public string FleetType { get; set; }

        /// <summary>
        /// The status of this equipment. E.g., Rental, Inventory, etc.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Denotes if this equipment is new or used.
        /// </summary>
        public string NewUsed { get; set; }

        /// <summary>
        /// The list price of this equipment.
        /// </summary>
        public decimal ListPrice { get; set; }

        /// <summary>
        /// The sell price of this equipment.
        /// </summary>
        public decimal SellPrice { get; set; }

        /// <summary>
        /// The date this equipment was acquired.
        /// </summary>
        public DateTime? AcquisitionDate { get; set; }

        /// <summary>
        /// Denotes if this equipment is available for a quote.
        /// </summary>
        public bool? AvailableForQuote { get; set; }

        /// <summary>
        /// Inventory notes for this equipment.
        /// </summary>
        public string InventoryNotes { get; set; }

        /// <summary>
        /// The class of this equipment.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Denotes whether this equipment is in or out.
        /// </summary>
        public string InOut { get; set; }

        /// <summary>
        /// The last service, repair, & maintenance cost for this equipment.
        /// </summary>
        public decimal LastSmr { get; set; }

        /// <summary>
        /// Html snippet containing the  inventory specs for this equipment.
        /// </summary>
        public string InventorySpecs { get; set; }

        /// <summary>
        /// The category for this equipment
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The name of the branch 
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// The combination of the make and model for this equipment.
        /// </summary>
        public string FullModel => $"{Make} {Model}";

        /// <summary>
        /// The combination of the make, model, and description for this equipment.
        /// </summary>
        public string MakeModelDescription => $"{Make} {Model} {Description}";

        /// <summary>
        /// The combination of the fleet and fleet type for this equipment.
        /// </summary>
        public string FleetCombined => $"{Fleet} {FleetType}";
    }

    public class GetEquipmentDto : BaseEquipmentDto
    {
        /// <summary>
        /// The unique identifier for this equipment entity.
        /// </summary>
        public int EquipmentId { get; set; }
    }
}
