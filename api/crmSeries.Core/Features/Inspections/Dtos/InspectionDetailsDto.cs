namespace crmSeries.Core.Features.Inspections.Dtos
{
    public class InspectionGroupDto
    {
        /// <summary>
        /// The identifier of the group's Inspection.
        /// </summary>
        public int InspectionId { get; set; }

        /// <summary>
        /// The name of the group.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// The inspection group's sequence.
        /// </summary>
        public int Sequence { get; set; }
    }

    public class InspectionItemDto
    {
        /// <summary>
        /// The identifier of the item's Group.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// The item's sequence.
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// The data type for this item. E.g., None, Number, Single-Selection, Text, YesNo, etc.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Denotes whether a response is required.
        /// </summary>
        public bool RequireResponse { get; set; }

        /// <summary>
        /// Denotes whether an image is required.
        /// </summary>
        public bool RequireImage { get; set; }

        /// <summary>
        /// Denotes whether a comment is required.
        /// </summary>
        public bool RequireComment { get; set; }

        /// <summary>
        /// The requirement filter.
        /// </summary>
        public string RequirementFilter { get; set; }
    }

    public class InspectionImageDto
    {
        /// <summary>
        /// The identifier of the image's Inspection
        /// </summary>
        public int InspectionId { get; set; }

        /// <summary>
        /// The image's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The image's sequence
        /// </summary>
        public int Sequence { get; set; }
    }

    public class InspectionResponseDto
    {
        /// <summary>
        /// The identifier of the response's Item
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// The response value. E.g., Good, Bad, Abnormal, Cracked, etc.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// The response sequence.
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Denotes whether an image is required.
        /// </summary>
        public bool RequireImage { get; set; }
    }

    public class GetInspectionGroupDto : InspectionGroupDto
    {
        /// <summary>
        /// The group's identifier.
        /// </summary>
        public int GroupId { get; set; }
    }

    public class GetInspectionItemDto : InspectionItemDto
    {
        /// <summary>
        /// The item's identifier
        /// </summary>
        public int ItemId { get; set; }
    }

    public class GetInspectionImageDto : InspectionImageDto
    {
        /// <summary>
        /// The image's identifier
        /// </summary>
        public int ImageId { get; set; }
    }

    public class GetInspectionResponseDto : InspectionResponseDto
    {
        /// <summary>
        /// The response's identifier.
        /// </summary>
        public int ResponseId { get; set; }
    }
}
