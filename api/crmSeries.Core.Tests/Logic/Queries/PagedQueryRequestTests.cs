using System;
using crmSeries.Core.Common;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Logic.Queries
{
    [TestFixture]
    public class PagedQueryRequestTests
    {
        [TestCase(1, 1)]
        [TestCase(100, 100)]
        [TestCase(101, Constants.MaxPageSize)]
        [TestCase(Int32.MaxValue, Constants.MaxPageSize)]
        public void Initialization_PageSizeSetToVariousValues_PageSizeNeverExceedsMaxPageSize(
            int requestedPageSize,
            int actualPageSize)
        {
            // Arrange
            var pageQueryRequest = new PagedQueryRequest
            {
                PageNumber = 1,
                PageSize = requestedPageSize
            };

            // Assert
            Assert.AreEqual(actualPageSize, pageQueryRequest.PageSize);
        }
    }
}
