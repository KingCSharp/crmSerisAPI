using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.CompanySources.Dtos;

namespace crmSeries.Core.Features.CompanySources.Mapping
{
    public class CompanySourceDtoAutoMapperConfigurator : Profile
    {
        public CompanySourceDtoAutoMapperConfigurator()
        {
            CreateMap<CompanySource, GetCompanySourceDto>();
            CreateMap<GetCompanySourceDto, CompanySource>();
        }
    }
}
