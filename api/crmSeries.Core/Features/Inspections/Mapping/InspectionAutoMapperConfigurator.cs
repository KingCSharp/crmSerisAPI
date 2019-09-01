using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Inspections.Dtos;

namespace crmSeries.Core.Inspections.Mapping
{
    public class InspectionTypeMapperConfigurator : Profile
    {
        public InspectionTypeMapperConfigurator()
        {
            CreateMap<InspectionType, BaseInspectionTypeDto>()
                .ForMember(x => x.InspectionTypeId, options => options.MapFrom(s => s.TypeId))
                .ForMember(x => x.InspectionTypeName, options => options.MapFrom(s => s.Type))
                .ForMember(x => x.Inspections, options => options.Ignore());

            CreateMap<InspectionType, GetInspectionTypeDto>()
                .IncludeBase<InspectionType, BaseInspectionTypeDto>();
        }
    }

    public class InspectionMapperConfigurator : Profile
    {
        public InspectionMapperConfigurator()
        {
            CreateMap<Inspection, BaseInspectionDto>()
                .ForMember(x => x.InspectionName, options => options.MapFrom(s => s.Name));

            CreateMap<Inspection, GetInspectionDto>()
                .IncludeBase<Inspection, BaseInspectionDto>();
        }
    }

    public class InspectionDetailsMapperConfigurator : Profile
    {
        public InspectionDetailsMapperConfigurator()
        {
            CreateMap<InspectionGroup, InspectionGroupDto>();

            CreateMap<InspectionImage, InspectionImageDto>();

            CreateMap<InspectionItem, InspectionItemDto>();

            CreateMap<InspectionResponse, InspectionResponseDto>();

            CreateMap<InspectionGroup, GetInspectionGroupDto>()
                .IncludeBase<InspectionGroup, InspectionGroupDto>();

            CreateMap<InspectionImage, GetInspectionImageDto>()
                .IncludeBase<InspectionImage, InspectionImageDto>();

            CreateMap<InspectionItem, GetInspectionItemDto>()
                .IncludeBase<InspectionItem, InspectionItemDto>();

            CreateMap<InspectionResponse, GetInspectionResponseDto>()
                .IncludeBase<InspectionResponse, InspectionResponseDto>();
        }
    }

    public class InspectionTemplateMapperConfigurator : Profile
    {
        public InspectionTemplateMapperConfigurator()
        {
            CreateMap<Inspection, GetInspectionTemplateDto>()
                .ForMember(x => x.Groups, options => options.Ignore())
                .ForMember(x => x.Images, options => options.Ignore());

            CreateMap<InspectionGroup, GetInspectionTemplateGroupDto>()
                .IncludeBase<InspectionGroup, GetInspectionGroupDto>()
                .ForMember(x => x.Items, options => options.Ignore());

            CreateMap<InspectionItem, GetInspectionTemplateItemDto>()
                .IncludeBase<InspectionItem, GetInspectionItemDto>()
                .ForMember(x => x.Responses, options => options.Ignore());

            CreateMap<InspectionResponse, GetInspectionTemplateResponseDto>()
                .IncludeBase<InspectionResponse, GetInspectionResponseDto>();

            CreateMap<InspectionImage, GetInspectionTemplateImageDto>()
                .IncludeBase<InspectionImage, GetInspectionImageDto>();
        }
    }

    public class RecordAssignedInspectionMapperConfigurator : Profile
    {
        public RecordAssignedInspectionMapperConfigurator()
        {
            CreateMap<BaseRecordAssignedInspectionDto, RecordAssignedInspection>()
                .ForMember(x => x.AssignedInspectionId, options => options.Ignore())
                .ForMember(x => x.UserId, options => options.Ignore())
                .ForMember(x => x.InspectionNo, options => options.Ignore())
                .ForMember(x => x.Complete, options => options.Ignore())
                .ForMember(x => x.IncludeReconditionAmount, options => options.Ignore())
                .ForMember(x => x.Deleted, options => options.MapFrom(_ => false));

            CreateMap<RecordAssignedInspectionDto, RecordAssignedInspection>()
                .IncludeBase<BaseRecordAssignedInspectionDto, RecordAssignedInspection>();

            CreateMap<RecordAssignedInspection, GetRecordAssignedInspectionDto>()
                .ForMember(x => x.Groups, options => options.Ignore());

            CreateMap<BaseRecordAssignedInspectionGroupDto, RecordAssignedInspectionGroup>()
                .ForMember(x => x.AssignedGroupId, options => options.Ignore())
                .ForMember(x => x.AssignedInspectionId, options => options.Ignore());

            CreateMap<RecordAssignedInspectionGroupDto, RecordAssignedInspectionGroup>()
                .IncludeBase<BaseRecordAssignedInspectionGroupDto, RecordAssignedInspectionGroup>();

            CreateMap<RecordAssignedInspectionGroup, GetRecordAssignedInspectionGroupDto>()
                .ForMember(x => x.Items, options => options.Ignore());
            
            CreateMap<BaseRecordAssignedInspectionItemDto, RecordAssignedInspectionItem>()
                .ForMember(x => x.AssignedItemId, options => options.Ignore())
                .ForMember(x => x.AssignedGroupId, options => options.Ignore())
                .ForMember(x => x.RequirementFilter, options => options.Ignore())
                .ForMember(x => x.ReconditionAmount, options => options.Ignore());

            CreateMap<RecordAssignedInspectionItemDto, RecordAssignedInspectionItem>()
                .IncludeBase<BaseRecordAssignedInspectionItemDto, RecordAssignedInspectionItem>();

            CreateMap<RecordAssignedInspectionItem, GetRecordAssignedInspectionItemDto>()
                .ForMember(x => x.Responses, options => options.Ignore());

            CreateMap<BaseRecordAssignedInspectionItemResponseDto, RecordAssignedInspectionItemResponse>()
                .ForMember(x => x.ResponseId, options => options.Ignore())
                .ForMember(x => x.AssignedItemId, options => options.Ignore());

            CreateMap<RecordAssignedInspectionItemResponseDto, RecordAssignedInspectionItemResponse>()
                .IncludeBase<BaseRecordAssignedInspectionItemResponseDto, RecordAssignedInspectionItemResponse>();

            CreateMap<RecordAssignedInspectionItemResponse, GetRecordAssignedInspectionItemResponseDto>();

            CreateMap<RecordAssignedInspectionItemImageDto, RecordAssignedInspectionImage>()
                .ForMember(x => x.AssignedInspectionId, options => options.MapFrom(x => x.AssignedInspectionId))
                .ForMember(x => x.AssignedItemId, options => options.MapFrom(x => x.AssignedItemId))
                .IgnoreRest();

            CreateMap<RecordAssignedInspectionImage, GetRecordAssignedInspectionItemImageDto>();
        }
    }
}