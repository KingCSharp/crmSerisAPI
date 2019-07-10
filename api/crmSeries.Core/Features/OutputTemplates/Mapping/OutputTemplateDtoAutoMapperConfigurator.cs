using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.OutputTemplates.Dtos;

namespace crmSeries.Core.Features.OutputTemplates.Mapping
{
    public class OutputTemplateDtoAutoMapperConfigurator : Profile
    {
        public OutputTemplateDtoAutoMapperConfigurator()
        {
            CreateMap<OutputTemplate, GetOutputTemplateDto>();
            CreateMap<GetOutputTemplateDto, OutputTemplate>();

            CreateMap<OutputTemplate, AddOutputTemplateDto>();
            CreateMap<AddOutputTemplateDto, OutputTemplate>();

            //CreateMap<AddOutputTemplateCategoryRequest, OutputTemplateCategory>()
            //    .ForMember(x => x.CompanyId, options => options.Ignore())
            //    .ForMember(x => x.Deleted, options => options.Ignore())
            //    .ForMember(x => x.LastModified, options => options.Ignore());

            //CreateMap<EditOutputTemplateCategoryRequest, OutputTemplateCategory>()
            //    .ForMember(x => x.LastModified, options => options.Ignore());
        }
    }
}