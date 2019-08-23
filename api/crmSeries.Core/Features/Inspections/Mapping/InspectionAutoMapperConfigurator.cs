using AutoMapper;
using crmSeries.Core.Features.Inspections.Dtos;

namespace crmSeries.Core.Inspections.Mapping
{
    public class InspectionTypeMapperConfigurator : Profile
    {
        public InspectionTypeMapperConfigurator()
        {
            CreateMap<Domain.HeavyEquipment.InspectionType, GetInspectionTypeDto>()
                .ForMember(x => x.InspectionTypeId, options => options.MapFrom(s => s.TypeId))
                .ForMember(x => x.InspectionTypeName, options => options.MapFrom(s => s.Type));
        }
    }

    public class InspectionMapperConfigurator : Profile
    {
        public InspectionMapperConfigurator()
        {
            CreateMap<Domain.HeavyEquipment.Inspection, GetInspectionDto>()
                .ForMember(x => x.InspectionId, options => options.MapFrom(s => s.InspectionId))
                .ForMember(x => x.InspectionName, options => options.MapFrom(s => s.Name));


        }
    }

    public class RecordAssignedInspectionMapperConfigurator : Profile
    {
        public RecordAssignedInspectionMapperConfigurator()
        {
            CreateMap<RecordAssignInspectionDto, Domain.HeavyEquipment.RecordAssignedInspection>()
                .ForMember(x => x.AssignedInspectionId, options => options.Ignore())
                .ForMember(x => x.InspectionId, options => options.MapFrom(s => s.InspectionId))
                .ForMember(x => x.RecordId, options => options.MapFrom(s => s.RecordId))
                .ForMember(x => x.RecordType, options => options.MapFrom(s => s.RecordType))
                .ForMember(x => x.UserId, options => options.MapFrom(s => s.UserId))
                .ForMember(x => x.InspectionName, options => options.MapFrom(s => s.InspectionName))
                .ForMember(x => x.InspectionType, options => options.MapFrom(s => s.InspectionType))
                .ForMember(x => x.InspectionNo, options => options.MapFrom(s => s.InspectionNo))
                .ForMember(x => x.InspectionHours, options => options.MapFrom(s => s.InspectionHours))
                .ForMember(x => x.InspectionDate, options => options.MapFrom(s => s.InspectionDate))
                .ForMember(x => x.Comments, options => options.MapFrom(s => s.Comments))
                .ForMember(x => x.Complete, options => options.MapFrom(s => s.Complete))
                .ForMember(x => x.Deleted, options => options.MapFrom(s => s.Deleted))
                .ForMember(x => x.IncludeReconditionAmount, options => options.Ignore());

        }
    }
    public class RecordAssignedInspectionGroupMapperConfigurator : Profile
    {
        public RecordAssignedInspectionGroupMapperConfigurator()
        {
            CreateMap<RecordAssignedInspectionGroupDto, Domain.HeavyEquipment.RecordAssignedInspectionGroup>()
                .ForMember(x => x.AssignedGroupId, options => options.Ignore())
                .ForMember(x => x.AssignedInspectionId, options => options.MapFrom(s => s.AssignedInspectionId))
                .ForMember(x => x.GroupName, options => options.MapFrom(s => s.GroupName))
                .ForMember(x => x.Sequence, options => options.MapFrom(s => s.Sequence))
                .ForMember(x => x.Comments, options => options.MapFrom(s => s.Comments));


        }
    }
    public class RecordAssignedInspectionImageDtoMapperConfigurator : Profile
    {
        public RecordAssignedInspectionImageDtoMapperConfigurator()
        {
            CreateMap<RecordAssignedInspectionImageDto, Domain.HeavyEquipment.RecordAssignedInspectionImage>()
                .ForMember(x => x.ImageId, options => options.Ignore())
                .ForMember(x => x.AssignedInspectionId, options => options.MapFrom(s => s.AssignedInspectionId))
                .ForMember(x => x.AssignedItemId, options => options.MapFrom(s => s.AssignedItemId))
                // .ForMember(x => x.ImagePath, options => options.MapFrom(s => s.ImagePath))
                .ForMember(x => x.FileName, options => options.MapFrom(s => s.FileName));

        }
    }
    public class RecordAssignedInspectionItemDtoMapperConfigurator : Profile
    {
        public RecordAssignedInspectionItemDtoMapperConfigurator()
        {
            CreateMap<RecordAssignedInspectionItemDto, Domain.HeavyEquipment.RecordAssignedInspectionItem>()
                .ForMember(x => x.AssignedItemId, options => options.Ignore())
                .ForMember(x => x.AssignedGroupId, options => options.MapFrom(s => s.AssignedGroupId))
                .ForMember(x => x.Item, options => options.MapFrom(s => s.Item))
                .ForMember(x => x.Sequence, options => options.MapFrom(s => s.Sequence))
                .ForMember(x => x.DataType, options => options.MapFrom(s => s.DataType))
                .ForMember(x => x.Response, options => options.Ignore())
                .ForMember(x => x.Comments, options => options.Ignore())
                .ForMember(x => x.RequireResponse, options => options.MapFrom(s => s.RequireResponse))
                .ForMember(x => x.RequireImage, options => options.MapFrom(s => s.RequireImage))
                .ForMember(x => x.RequireComment, options => options.MapFrom(s => s.RequireComment))
                .ForMember(x => x.RequirementFilter, options => options.MapFrom(s => s.RequirementFilter))
                .ForMember(x => x.ReconditionAmount, options => options.MapFrom(s => s.ReconditionAmount));

        }
    }
    public class RecordAssinedInspectionItemResponseDtoMapperConfigurator : Profile
    {
        public RecordAssinedInspectionItemResponseDtoMapperConfigurator()
        {
            CreateMap<RecordAssinedInspectionItemResponseDto, Domain.HeavyEquipment.RecordAssignedInspectionItemResponse>()
                .ForMember(x => x.ResponseId, options => options.Ignore())
                .ForMember(x => x.AssignedItemId, options => options.MapFrom(s => s.AssignedItemId))
                .ForMember(x => x.Response, options => options.MapFrom(s => s.Response))
                .ForMember(x => x.Sequence, options => options.MapFrom(s => s.Sequence));

        }
    }
}