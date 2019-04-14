using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class UserRight
    {
        public int RightId { get; set; }
        public int CategoryId { get; set; }
        public string RightName { get; set; }
        public string DisplayText { get; set; }
        public int SortOrder { get; set; }
    }
}
