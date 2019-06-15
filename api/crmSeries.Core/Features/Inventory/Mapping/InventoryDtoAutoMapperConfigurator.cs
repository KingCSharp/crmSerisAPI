using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;

namespace crmSeries.Core.Features.Inventory.Mapping
{
    public class InventoryDtoAutoMapperConfigurator : Profile
    {
        public InventoryDtoAutoMapperConfigurator()
        {
            CreateMap<Equipment, GetInventoryDto>();
        }
    }
}
