using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Dtos;

namespace crmSeries.Core.Features.Companies.Mapping
{
    public class CompanyDtoAutoMapperConfigurator : Profile
    {
        public CompanyDtoAutoMapperConfigurator()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();
        }
    }
}