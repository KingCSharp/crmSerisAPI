using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.Equipment.Utility
{
    public static class EquipmentConstants
    {
        public static class ErrorMessages
        {
            public const string EquipmentNotFound = "No equipment with this id found.";
            public static readonly string ExceededStatusMaxLength = $"All statuses must be {StatusMaxLength} characters or less.";
        }

        public const int StatusMaxLength = 50;
    }
}
