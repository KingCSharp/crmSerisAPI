using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.Leads.Utility
{
    public static class LeadsConstants
    {
        public static class ErrorMessages
        {
            public const string EmailAddressInvalid = "The Email field is invalid.";
            public const string PhoneInvalid = "The Phone field is invalid.";
            public const string CellInvalid = "The Cell field is invalid.";
            public const string FaxInvalid = "The Fax field is invalid.";
            public const string CompanyPhoneInvalid = "The Company Phone field is invalid.";
            public const string PhoneOrEmailRequired = "You must submit either phone number or E-Mail.";
        }
    }
}
