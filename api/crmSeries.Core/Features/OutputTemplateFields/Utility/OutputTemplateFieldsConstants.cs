using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.OutputTemplateFields.Utility
{
    public static class OutputTemplateFieldsConstants
    {
        public static class ErrorMessages
        {
            public const string OutputTemplateFieldNotFound = "No output template field with this id found.";
        }

        public static class FieldTypes
        {
            public const string Approve = "Approve";
            public const string Checkbox = "Checkbox";
            public const string Company = "Company";
            public const string DateSigned = "DateSigned";
            public const string Date = "Date";
            public const string Decline = "Decline";
            public const string EmailAddress = "EmailAddress";
            public const string Email = "Email";
            public const string EnvelopeId = "EnvelopeId";
            public const string FirstName = "FirstName";
            public const string Formula = "Formula";
            public const string FullName = "FullName";
            public const string InitialHere = "InitialHere";
            public const string LastName = "LastName";
            public const string List = "List";
            public const string Notarize = "Notarize";
            public const string Note = "Note";
            public const string Number = "Number";
            public const string RadioGroup = "RadioGroup";
            public const string SignerAttachment = "SignerAttachment";
            public const string SignHere = "SignHere";
            public const string SmartSection = "SmartSection";
            public const string Ssn = "Ssn";
            public const string Text = "Text";
            public const string Title = "Title";
            public const string View = "View";
            public const string Zip = "Zip";
        }

        public const int MaxLengthTemplateField = 50;
        public const int MaxLengthFieldType = 50;
        public const int MaxLengthCrmSeriesField = 1000;
    }
}
