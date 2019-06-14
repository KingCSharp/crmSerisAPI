using System;

namespace crmSeries.Core.Features.Equipment
{
    public class BaseEquipmentDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Machine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Make { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Model { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string StockNo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string SerialNo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EquipYear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Fleet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FleetType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NewUsed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ListPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SellPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AcquisitionDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? AvailableForQuote { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string InventoryNotes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string InOut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal LastSmr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string InventorySpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FullModel => $"{Make} {Model}";

        /// <summary>
        /// 
        /// </summary>
        public string MakeModelDescription => $"{Make} {Model} {Description}";

        /// <summary>
        /// 
        /// </summary>
        public string FleetCombined => $"{Fleet} {FleetType}";
    }

    public class GetEquipmentDto : BaseEquipmentDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int EquipmentId { get; set; }
    }
}
