using AutoMapper;

namespace crmSeries.Core.Features.Equipment.Mapping
{
    public class EquipmentDtoAutoMapperConfigurator : Profile
    {
        public EquipmentDtoAutoMapperConfigurator()
        {
            CreateMap<Domain.HeavyEquipment.Equipment, GetEquipmentDto>();
        }
    }
}
