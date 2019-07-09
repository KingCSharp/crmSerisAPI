using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.OutputTemplateCategories.Dtos;

namespace crmSeries.Core.Features.OutputTemplateCategories.Mapping
{
    public class OutputTemplateCategoryDtoAutoMapperConfigurator : Profile
    {
        public OutputTemplateCategoryDtoAutoMapperConfigurator()
        {
            CreateMap<OutputTemplateCategory, GetOutputTemplateCategoryDto>();
            CreateMap<GetOutputTemplateCategoryDto, OutputTemplateCategory>();

            //CreateMap<AddOutputTemplateCategoryRequest, OutputTemplateCategory>()
            //    .ForMember(x => x.CompanyId, options => options.Ignore())
            //    .ForMember(x => x.Deleted, options => options.Ignore())
            //    .ForMember(x => x.LastModified, options => options.Ignore());

            //CreateMap<EditOutputTemplateCategoryRequest, OutputTemplateCategory>()
            //    .ForMember(x => x.LastModified, options => options.Ignore());
        }
    }
}