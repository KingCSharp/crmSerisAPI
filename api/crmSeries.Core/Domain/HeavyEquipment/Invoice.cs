using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Invoice
    {
        public int AssignedId { get; set; }
        public int ParentId { get; set; }
        public string ParentType { get; set; }
        public string InvoiceType { get; set; }
        public string Status { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string EquipMake { get; set; }
        public string EquipModel { get; set; }
        public string EquipSerialNumber { get; set; }
        public string EquipStockNumber { get; set; }
        public string Comments { get; set; }
    }
}
