using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class OpportunityTransactionType
    {
        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
        public bool Deleted { get; set; }
    }
}
