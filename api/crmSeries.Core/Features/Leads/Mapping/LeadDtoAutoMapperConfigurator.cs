using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Dtos;

namespace crmSeries.Core.Features.Leads.Mapping
{
    public class LeadDtoAutoMapperConfigurator : Profile
    {
        public LeadDtoAutoMapperConfigurator()
        {
            CreateMap<AddLeadDto, Lead>();
        }
    }
}
