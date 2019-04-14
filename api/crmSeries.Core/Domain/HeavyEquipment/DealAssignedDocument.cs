using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedDocument
    {
        public int AssignedId { get; set; }
        public int DealId { get; set; }
        public int TemplateId { get; set; }
        public string Description { get; set; }
        public string AbsoluteUri { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public bool ManualUpload { get; set; }
        public bool Archived { get; set; }
        public bool Deleted { get; set; }
        public string DocumentType { get; set; }
        public string Comments { get; set; }
    }
}
