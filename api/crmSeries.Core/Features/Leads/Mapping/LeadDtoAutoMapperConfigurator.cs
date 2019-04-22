using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Leads.Dtos;

namespace crmSeries.Core.Features.Leads.Mapping
{
    public class LeadDtoAutoMapperConfigurator : Profile
    {
        public LeadDtoAutoMapperConfigurator()
        {
            CreateMap<AddLeadDto, Lead>()
                .ForMember(destination => destination.FirstName,
                    options => options.MapFrom(source => source.FirstName));

            CreateMap<AddLeadDto, Lead>()
                .ForMember(destination => destination.LastName,
                    options => options.MapFrom(source => source.LastName));

        }
    }
}
