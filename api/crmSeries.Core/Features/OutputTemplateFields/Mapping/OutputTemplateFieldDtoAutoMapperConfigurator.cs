using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.OutputTemplateFields.Dtos;

namespace crmSeries.Core.Features.OutputTemplateFields.Mapping
{
    public class OutputTemplateFieldDtoAutoMapperConfigurator : Profile
    {
        public OutputTemplateFieldDtoAutoMapperConfigurator()
        {
            CreateMap<OutputTemplateField, GetOutputTemplateFieldDto>();
            CreateMap<GetOutputTemplateFieldDto, OutputTemplateField>();

            CreateMap<OutputTemplateField, AddOutputTemplateFieldDto>();
            CreateMap<AddOutputTemplateFieldDto, OutputTemplateField>();
        }
    }
}