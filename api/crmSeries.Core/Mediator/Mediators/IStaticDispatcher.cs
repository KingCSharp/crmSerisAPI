using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Mediator.Mediators
{
    public interface IStaticDispatcher
    {
        object Dispatch(object request);
    }
}
