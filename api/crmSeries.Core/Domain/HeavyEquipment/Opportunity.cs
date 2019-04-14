using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Opportunity
    {
        public int OpportunityId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int ContactId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CloseDate { get; set; }
        public decimal Probability { get; set; }
        public bool Deleted { get; set; }
        public DateTime OpenDate { get; set; }
    }
}
