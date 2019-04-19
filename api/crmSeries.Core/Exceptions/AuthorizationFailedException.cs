using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace crmSeries.Core.Exceptions
{
    public class AuthorizationFailedException : SecurityException
    {
        public AuthorizationFailedException()
        {
        }

        public AuthorizationFailedException(string message) : base(message)
        {
        }
    }
}
