using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Branches.Dtos;

namespace crmSeries.Core.Features.Branches.Mapping
{
    public class BranchDtoAutoMapperConfigurator : Profile
    {
        public BranchDtoAutoMapperConfigurator()
        {
            CreateMap<Branch, GetBranchDto>();
            CreateMap<GetBranchDto, Branch>();
        }
    }
}
