using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.CompanyAssignedAddresses.Dtos;

namespace crmSeries.Core.Features.CompanyAssignedAddresses.Mapping
{
    public class CompanyAssignedAddressDtoAutoMapperConfigurator : Profile
    {
        public CompanyAssignedAddressDtoAutoMapperConfigurator()
        {
            CreateMap<CompanyAssignedAddress, CompanyAssignedAddressDto>();
            CreateMap<CompanyAssignedAddressDto, CompanyAssignedAddress>();
        }
    }
}
