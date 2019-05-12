using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Logic.Queries
{
    public class PagedQueryRequest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
