﻿using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Logic.Queries
{
    /// <summary>
    /// Request object for a paginated list of results.
    /// </summary>
    public class PagedQueryRequest
    {
        private const int _maxPageSize = 100;

        /// <summary>
        /// The page number for the paginated results.
        /// Setting a number beyond the last page will just return the last page of data.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of items returned for the page. Maximum size of 100.
        /// </summary>
        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = Math.Min(value, _maxPageSize); }
        }
    }
}
