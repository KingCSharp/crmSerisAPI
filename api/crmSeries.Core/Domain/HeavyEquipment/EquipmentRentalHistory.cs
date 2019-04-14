using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentRentalHistory
    {
        public int HistoryId { get; set; }
        public int EquipmentId { get; set; }
        public int CompanyId { get; set; }
        public bool CurrentRental { get; set; }
        public string ContractNumber { get; set; }
        public string ContractType { get; set; }
        public DateTime DateOnRent { get; set; }
        public DateTime? ExpectedOffRent { get; set; }
        public DateTime? DateOffRent { get; set; }
        public decimal RentalRate { get; set; }
    }
}
