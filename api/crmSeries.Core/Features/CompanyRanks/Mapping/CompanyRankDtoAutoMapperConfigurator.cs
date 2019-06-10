using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.CompanyRanks.Dtos;

namespace crmSeries.Core.Features.CompanyRanks.Mapping
{
    public class CompanyRankDtoAutoMapperConfigurator : Profile
    {
        public CompanyRankDtoAutoMapperConfigurator()
        {
            CreateMap<CompanyRank, GetCompanyRankDto>();
            CreateMap<GetCompanyRankDto, CompanyRank>();
        }
    }
}
