using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentComponentCriteria
    {
        public int CriteriaId { get; set; }
        public int ProfileId { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public int Hours { get; set; }
        public string Category { get; set; }
        public string EnterBy { get; set; }
        public DateTime EnterDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
