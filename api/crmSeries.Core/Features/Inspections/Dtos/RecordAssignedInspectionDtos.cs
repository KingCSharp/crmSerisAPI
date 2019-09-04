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

        /// <summary>
        /// The previously assigned identifier of this inspection record if editing an existing record
        /// </summary>
        public int? AssignedInspectionId { get; set; }

        /// <summary>
        /// The inspection data
        /// </summary>
        public RecordAssignedInspectionDto Inspection { get; set; }
    }

    public class BaseRecordAssignedInspectionDto
    {
        /// <summary>
        /// The identifier of the record's Inspection.
        /// </summary>
        public int InspectionId { get; set; }

        /// <summary>
        /// The identifier of the related record.
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// The type of the related record.
        /// </summary>
        public string RecordType { get; set; }

        /// <summary>
        /// The name of the record's Inspection.
        /// </summary>
        public string InspectionName { get; set; }

        /// <summary>
        /// The type of the record's Inspection.
        /// </summary>
        public string InspectionType { get; set; }

        /// <summary>
        /// Date the inspection was created.
        /// </summary>
        public DateTime InspectionDate { get; set; }

        /// <summary>
        /// Hours on the equipment when the inspection was created.
        /// </summary>
        public decimal InspectionHours { get; set; }

        /// <summary>
        /// Overall comments from the inspection.
        /// </summary>
        public string Comments { get; set; }
    }

    public class BaseRecordAssignedInspectionGroupDto
    {
        /// <summary>
        /// The name of the record's Group.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// The sequence of the record's Group.
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Comments that the user added for the group.
        /// </summary>
        public string Comments { get; set; }
    }

    public class BaseRecordAssignedInspectionItemDto
    {
        /// <summary>
        /// The name of the record's Item.
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// The sequence of the record's Item.
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// The data type of the record's Item.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// The user's response.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Comments the user added for the item.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Whether or not the record's Item requires a response.
        /// </summary>
        public bool RequireResponse { get; set; }

        /// <summary>
        /// Whether or not the record's Item requires an image.
        /// </summary>
        public bool RequireImage { get; set; }

        /// <summary>
        /// Whether or not the record's Item requires a comment.
        /// </summary>
        public bool RequireComment { get; set; }
    }

    public class BaseRecordAssignedInspectionItemResponseDto
    {
        /// <summary>
        /// The response of the record's Item Response.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// The sequence of the record's Item Response.
        /// </summary>
        public int Sequence { get; set; }
    }
    
    public class RecordAssignedInspectionDto : BaseRecordAssignedInspectionDto
    {
        /// <summary>
        /// The inspection's groups.
        /// </summary>
        public List<RecordAssignedInspectionGroupDto> Groups { get; set; } = 
            new List<RecordAssignedInspectionGroupDto>();
    }

    public class RecordAssignedInspectionGroupDto : BaseRecordAssignedInspectionGroupDto
    {
        /// <summary>
        /// The groups's items.
        /// </summary>
        public List<RecordAssignedInspectionItemDto> Items { get; set; } = 
            new List<RecordAssignedInspectionItemDto>();
    }

    public class RecordAssignedInspectionItemDto : BaseRecordAssignedInspectionItemDto
    {
        /// <summary>
        /// The item's responses.
        /// </summary>
        public List<RecordAssignedInspectionItemResponseDto> Responses { get; set; } = 
            new List<RecordAssignedInspectionItemResponseDto>();
    }

    public class RecordAssignedInspectionItemResponseDto : BaseRecordAssignedInspectionItemResponseDto
    {
    }

    public class GetRecordAssignedInspectionDto : BaseRecordAssignedInspectionDto
    {
        /// <summary>
        /// The identifier assigned to this record.
        /// </summary>
        public int AssignedInspectionId { get; set; }

        /// <summary>
        /// The recorded inspection's groups.
        /// </summary>
        public List<GetRecordAssignedInspectionGroupDto> Groups { get; set; } =
            new List<GetRecordAssignedInspectionGroupDto>();
    }

    public class GetRecordAssignedInspectionGroupDto : BaseRecordAssignedInspectionGroupDto
    {
        /// <summary>
        /// The identifier assigned to this record.
        /// </summary>
        public int AssignedGroupId { get; set; }

        /// <summary>
        /// The assigned identifier of the inspection record that is the parent of this record.
        /// </summary>
        public int AssignedInspectionId { get; set; }

        /// <summary>
        /// The recorded group's items.
        /// </summary>
        public List<GetRecordAssignedInspectionItemDto> Items { get; set; } = 
            new List<GetRecordAssignedInspectionItemDto>();
    }

    public class GetRecordAssignedInspectionItemDto : BaseRecordAssignedInspectionItemDto
    {
        /// <summary>
        /// The identifier assigned to this record.
        /// </summary>
        public int AssignedItemId { get; set; }

        /// <summary>
        /// The assigned identifier of the group record that is the parent of this record.
        /// </summary>
        public int AssignedGroupId { get; set; }

        /// <summary>
        /// The recorded item's responses
        /// </summary>
        public List<GetRecordAssignedInspectionItemResponseDto> Responses { get; set; } =
            new List<GetRecordAssignedInspectionItemResponseDto>();
    }

    public class GetRecordAssignedInspectionItemResponseDto : BaseRecordAssignedInspectionItemResponseDto
    {
        /// <summary>
        /// The identifier assigned to this record.
        /// </summary>
        public int ResponseId { get; set; }

        /// <summary>
        /// The assigned identifier of the item record that is the parent of this record.
        /// </summary>
        public int AssignedItemId { get; set; }
    }

    public class RecordAssignedInspectionItemImageDto
    {
        /// <summary>
        /// The identifier of the recorded inspection to which the item belongs.
        /// </summary>
        public int AssignedInspectionId { get; set; }

        /// <summary>
        /// The identifier of the recorded item to which this image should be attached.
        /// </summary>
        public int AssignedItemId { get; set; }

        /// <summary>
        /// The file name of the image.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///  The file size (in bytes) of the image.
        /// </summary>
        public int FileLength { get; set; }

        /// <summary>
        /// A readable stream of the image's data.
        /// </summary>
        public Stream FileStream { get; set; }
    }

    public class GetRecordAssignedInspectionItemImageDto
    {
        /// <summary>
        /// The identifier assigned to this record.
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        /// The identifier of the recorded inspection to which the item belongs.
        /// </summary>
        public int AssignedInspectionId { get; set; }

        /// <summary>
        /// The identifier of the recorded item to which this image is attached.
        /// </summary>
        public int AssignedItemId { get; set; }

        /// <summary>
        /// An accessible url to view or download the attached image.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// The image's file name.
        /// </summary>
        public string FileName { get; set; }
    }
}
