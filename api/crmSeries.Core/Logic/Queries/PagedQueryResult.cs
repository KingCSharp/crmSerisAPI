using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Logic.Queries
{
    public class PagedQueryResult<T>
    {
        private List<T> _items;
        public List<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }

        public int TotalItemCount { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int PageCount { get; set; }
    }
}
