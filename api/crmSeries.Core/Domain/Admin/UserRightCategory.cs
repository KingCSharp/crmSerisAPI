using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class UserRightCategory
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int SortOrder { get; set; }
    }
}
