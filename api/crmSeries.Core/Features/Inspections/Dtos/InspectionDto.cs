namespace crmSeries.Core.Features.Inspections.Dtos
{
    public class BaseInspectionDto
    {
        /// <summary>
        /// The Inspection identifier.
        /// </summary>
        public int InspectionId { get; set; }

        /// <summary>
        /// The name of the Inspection. 
        /// </summary>
        public string InspectionName { get; set; }
    }

    public class GetInspectionDto : BaseInspectionDto
    {

    }
}
