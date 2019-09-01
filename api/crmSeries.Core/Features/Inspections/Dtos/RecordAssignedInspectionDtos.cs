using System;
using System.Collections.Generic;
using System.IO;

namespace crmSeries.Core.Features.Inspections.Dtos
{
    public class SaveInspectionDto
    {
        public SaveInspectionDto()
        {
            AssignedInspectionId = null;
        }

        public SaveInspectionDto(int assignedInspectionId)
        {
            AssignedInspectionId = assignedInspectionId;
        }

        public int? AssignedInspectionId { get; set; }

        public RecordAssignedInspectionDto Inspection { get; set; }
    }

    public class BaseRecordAssignedInspectionDto
    {
        public int InspectionId { get; set; }

        public int RecordId { get; set; }

        public string RecordType { get; set; }

        public string InspectionName { get; set; }

        public string InspectionType { get; set; }

        public DateTime InspectionDate { get; set; }

        public decimal InspectionHours { get; set; }

        public string Comments { get; set; }
    }

    public class BaseRecordAssignedInspectionGroupDto
    {
        public string GroupName { get; set; }

        public int Sequence { get; set; }

        public string Comments { get; set; }
    }

    public class BaseRecordAssignedInspectionItemDto
    {
        public string Item { get; set; }

        public int Sequence { get; set; }

        public string DataType { get; set; }

        public string Response { get; set; }

        public string Comments { get; set; }

        public bool RequireResponse { get; set; }

        public bool RequireImage { get; set; }

        public bool RequireComment { get; set; }
    }

    public class BaseRecordAssignedInspectionItemResponseDto
    {
        public string Response { get; set; }

        public int Sequence { get; set; }
    }
    
    public class RecordAssignedInspectionDto : BaseRecordAssignedInspectionDto
    {
        public List<RecordAssignedInspectionGroupDto> Groups { get; set; } = 
            new List<RecordAssignedInspectionGroupDto>();
    }

    public class RecordAssignedInspectionGroupDto : BaseRecordAssignedInspectionGroupDto
    {
        public List<RecordAssignedInspectionItemDto> Items { get; set; } = 
            new List<RecordAssignedInspectionItemDto>();
    }

    public class RecordAssignedInspectionItemDto : BaseRecordAssignedInspectionItemDto
    {
        public List<RecordAssignedInspectionItemResponseDto> Responses { get; set; } = 
            new List<RecordAssignedInspectionItemResponseDto>();
    }

    public class RecordAssignedInspectionItemResponseDto : BaseRecordAssignedInspectionItemResponseDto
    {
    }

    public class GetRecordAssignedInspectionDto : BaseRecordAssignedInspectionDto
    {
        public int AssignedInspectionId { get; set; }

        public List<GetRecordAssignedInspectionGroupDto> Groups { get; set; } =
            new List<GetRecordAssignedInspectionGroupDto>();
    }

    public class GetRecordAssignedInspectionGroupDto : BaseRecordAssignedInspectionGroupDto
    {
        public int AssignedGroupId { get; set; }

        public int AssignedInspectionId { get; set; }

        public List<GetRecordAssignedInspectionItemDto> Items { get; set; } = 
            new List<GetRecordAssignedInspectionItemDto>();
    }

    public class GetRecordAssignedInspectionItemDto : BaseRecordAssignedInspectionItemDto
    {
        public int AssignedItemId { get; set; }

        public int AssignedGroupId { get; set; }

        public List<GetRecordAssignedInspectionItemResponseDto> Responses { get; set; } =
            new List<GetRecordAssignedInspectionItemResponseDto>();
    }

    public class GetRecordAssignedInspectionItemResponseDto : BaseRecordAssignedInspectionItemResponseDto
    {
        public int ResponseId { get; set; }

        public int AssignedItemId { get; set; }
    }

    public class RecordAssignedInspectionItemImageDto
    {
        public int AssignedInspectionId { get; set; }

        public int AssignedItemId { get; set; }

        public string FileName { get; set; }

        public int FileLength { get; set; }

        public Stream FileStream { get; set; }
    }

    public class GetRecordAssignedInspectionItemImageDto
    {
        public int ImageId { get; set; }

        public int AssignedInspectionId { get; set; }

        public int AssignedItemId { get; set; }

        public string ImagePath { get; set; }

        public string FileName { get; set; }
    }
}
