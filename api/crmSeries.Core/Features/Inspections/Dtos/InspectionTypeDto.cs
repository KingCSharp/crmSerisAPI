using System.Collections.Generic;

namespace crmSeries.Core.Features.Inspections.Dtos
{
    public class BaseInspectionTypeDto
    {
        /// <summary>
        /// The identifier of the InspectionType is classified as.
        /// </summary>
        public int InspectionTypeId { get; set; }
        /// <summary>
        /// The name of InspectionType. 
        /// </summary>
        public string InspectionTypeName { get; set; }

        public List<BaseInspectionDto> Inspections { get; set; }


    }

    public class GetInspectionTypeDto : BaseInspectionTypeDto
    {

    }
}
