using System.Collections.Generic;

namespace crmSeries.Core.Features.Equipment
{
    public class EquipmentConstants
    {
        public static class ErrorMessages
        {
            public static readonly string InvalidParentType = 
                $"Invalid Parent Type.  Valid priorities are {ParentTypesFlattened}.  You can also pass nothing as this is an optional field";
        }

        public const string Company = "Company";

        public static readonly IReadOnlyCollection<string> ParentTypes = new List<string>
        {
            Company,
        };

        private static readonly string ParentTypesFlattened =
            string.Join(",", ParentTypes);
    }
}
