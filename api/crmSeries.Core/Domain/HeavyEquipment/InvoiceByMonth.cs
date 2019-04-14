using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class InvoiceByMonth
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string ParentType { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceType { get; set; }
    }
}
