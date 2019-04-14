using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealStatus
    {
        public int StatusId { get; set; }
        public string Status { get; set; }
        public bool Fixed { get; set; }
        public bool CustomerResponse { get; set; }
        public string InternalStatus { get; set; }
        public bool Selectable { get; set; }
        public bool PostApproval { get; set; }
        public bool InitialApproved { get; set; }
        public decimal WinProbability { get; set; }
    }
}
