using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.OutputTemplates.Utility
{
    public static class OutputTemplatesConstants
    {
        public static class ErrorMessages
        {
            public const string OutputTemplateNotFound = "No output template with this id found.";
            public const string DocuSignOutputTemplateNotFound = "No output template with this DocuSign id found.";
        }

        public const int MaxLengthTemplate = 50;
        public const int MaxLengthTemplateType = 50;
        public const int MaxLengthDescription = 1000;
        public const int MaxLengthAbsoluteUri = 1000;
        public const int MaxLengthFileName = 1000;
        public const int MaxLengthContentType = 100;
        public const int MaxLengthSource = 50;
        public const int MaxLengthSourceId = 50;
    }
}
