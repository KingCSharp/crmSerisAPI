using crmSeries.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Tests.Mocks
{
    public class MockLogger : ILogger
    {
        public void Log(string log, params object[] data) { }
    }
}
