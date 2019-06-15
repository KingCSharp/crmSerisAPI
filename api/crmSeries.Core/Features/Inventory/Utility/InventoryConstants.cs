using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.Inventory.Utility
{
    public static class InventoryConstants
    {
        public static class ErrorMessages
        {
            public const string InventoryNotFound = "No inventory equipment with this id found.";
            public static readonly string ExceededStatusMaxLength = $"All statuses must be {StatusMaxLength} characters or less.";
        }

        public const int StatusMaxLength = 50;
    }
}
