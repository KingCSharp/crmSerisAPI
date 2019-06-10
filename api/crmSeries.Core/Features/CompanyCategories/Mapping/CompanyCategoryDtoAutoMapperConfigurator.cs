using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.CompanyCategories.Dtos;

namespace crmSeries.Core.Features.CompanyCategories.Mapping
{
    public class CompanyCategoryDtoAutoMapperConfigurator : Profile
    {
        public CompanyCategoryDtoAutoMapperConfigurator()
        {
            CreateMap<CompanyCategory, GetCompanyCategoryDto>();
            CreateMap<GetCompanyCategoryDto, CompanyCategory>();
        }
    }
}