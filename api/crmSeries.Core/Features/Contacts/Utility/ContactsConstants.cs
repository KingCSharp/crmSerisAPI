using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.Leads.Utility
{
    public static class ContactsConstants
    {
        public static class ErrorMessages
        {
            public const string ContactNotFound = "No contact with this id found.";
        }

        public const int MaxMiddleNameLength = 50;
        public const int MaxFirstNameLength = 50;
        public const int MaxLastNameLength = 50;
        public const int MaxNickNameLength = 50;
        public const int MaxTitleLength = 100;
        public const int MaxPositionLength = 100;
        public const int MaxDepartmentLength = 100;
    }
}
