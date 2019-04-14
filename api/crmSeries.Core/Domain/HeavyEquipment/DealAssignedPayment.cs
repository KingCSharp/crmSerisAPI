using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedPayment
    {
        public int PaymentId { get; set; }
        public int DealId { get; set; }
        public string Description { get; set; }
        public int Months { get; set; }
        public decimal Rate { get; set; }
        public decimal Payment { get; set; }
        public decimal TotalFinanceCharges { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public string EditType { get; set; }
        public bool Deleted { get; set; }
    }
}
