using System;
using System.Text;

namespace crmSeries.Core.Logging
{
    public interface ILogger
    {
        void Log(string log, params object[] data);
    }
}
