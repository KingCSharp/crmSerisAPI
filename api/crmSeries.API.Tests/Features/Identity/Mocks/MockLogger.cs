using System.Collections.Generic;
using crmSeries.Core.Logging;

namespace crmSeries.API.Tests.Features.Identity.Mocks
{
    public class MockLogger : ILogger
    {
        public List<LogMessage> Messages { get; } = new List<LogMessage>();

        public void Log(string log, params object[] data)
        {
            Messages.Add(new LogMessage { Message = log, Data = data });
        }

        public class LogMessage
        {
            public string Message { get; set; }

            public object[] Data { get; set; }
        }
    }
}


