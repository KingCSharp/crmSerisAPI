using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecordAssignedInspection
    {
        public int AssignedInspectionId { get; set; }
        public int InspectionId { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }
        public int UserId { get; set; }
        public string InspectionName { get; set; }
        public string InspectionType { get; set; }
        public string InspectionNo { get; set; }
        public DateTime InspectionDate { get; set; }
        public decimal InspectionHours { get; set; }
        public string Comments { get; set; }
        public bool Complete { get; set; }
        public bool Deleted { get; set; }
        public bool IncludeReconditionAmount { get; set; }
    }
}
