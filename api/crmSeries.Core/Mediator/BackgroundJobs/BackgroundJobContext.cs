using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Mediator.BackgroundJobs
{
    public class BackgroundJobContext
    {
        public bool IsBackgroundJob { get; }

        public BackgroundJobContext(bool isBackgroundJob)
        {
            IsBackgroundJob = isBackgroundJob;
        }
    }
}
