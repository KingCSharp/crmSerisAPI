using System.Collections.Generic;

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

    public class SaveInspectionDto
    {
        public RecordAssignInspectionDto inspection;
        public List<RecordAssignedInspectionGroupDto> group;
        public List<RecordAssignedInspectionImageDto> image;
        public List<RecordAssignedInspectionItemDto> inspectionItem;
        public List<RecordAssinedInspectionItemResponseDto> response;
    }
    
    public class AddInspectionDto : SaveInspectionDto
    {
    }
    
    public class RecordAssignInspectionDto
    {
        public int InspectionId { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }
        public int UserId { get; set; }
        public string InspectionName { get; set; }
        public string InspectionType { get; set; }
        public string InspectionNo { get; set; }
        public System.DateTime InspectionDate { get; set; }
        public decimal InspectionHours { get; set; }
        public string Comments { get; set; }
        public bool Complete { get; set; }
        public bool Deleted { get; set; }

    }

    public class RecordAssignedInspectionGroupDto
    {
        public int AssignedInspectionId { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int Sequence { get; set; }
        public string Comments { get; set; }
    }

    public class RecordAssignedInspectionImageDto
    {
        public int ImageId { get; set; }
        public int ItemId { get; set; }
        public int AssignedInspectionId { get; set; }
        public int AssignedItemId { get; set; }
        public string FileName { get; set; }
        public byte[] Str { get; set; }
        public string ContentType { get; set; }
    }

    public class RecordAssignedInspectionItemDto
    {
        public int AssignedGroupId { get; set; }
        public string Item { get; set; }
        public int ItemID { get; set; }
        public int GroupID { get; set; }
        public int Sequence { get; set; }
        public string DataType { get; set; }
        public bool RequireResponse { get; set; }
        public bool RequireImage { get; set; }
        public bool RequireComment { get; set; }
        public string RequirementFilter { get; set; }
        public decimal ReconditionAmount { get; set; }
    }

    public class RecordAssinedInspectionItemResponseDto
    {
        public int ResponseId { get; set; }
        public int AssignedItemId { get; set; }
        public int ItemId { get; set; }
        public string Response { get; set; }
        public int Sequence { get; set; }
    }
}
