using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.CompanyAssignedRanks.Dtos;

namespace crmSeries.Core.Features.CompanyAssignedRanks.Mapping
{
    public class CompanyAssignedRankDtoAutoMapperConfigurator : Profile
    {
        public CompanyAssignedRankDtoAutoMapperConfigurator()
        {
            CreateMap<CompanyAssignedRank, GetCompanyAssignedRankDto>();
            CreateMap<GetCompanyAssignedRankDto, CompanyAssignedRank>();

            CreateMap<AddCompanyAssignedRankRequest, CompanyAssignedRank>()
                .ForMember(x => x.AssignedId, options => options.Ignore());
        }
    }
}