using AutoMapper;
using crmSeries.Core.Features.Equipment.Dtos;

namespace crmSeries.Core.Equipment.Mapping
{
    public class EquipmentMapperConfigurator : Profile
    {
        public EquipmentMapperConfigurator()
        {
            CreateMap<Domain.HeavyEquipment.Equipment, GetEquipmentDto>()
                .ForMember(x => x.SerialNumber, options => options.MapFrom(s => s.SerialNo))
                .ForMember(x => x.StockNumber, options => options.MapFrom(s => s.StockNo))
                .ForMember(x => x.EquipmentNumber, options => options.MapFrom(s => s.EquipmentNo))
                .ForMember(x => x.EquipmentYear, options => options.MapFrom(s => s.EquipYear))
                .ForMember(x => x.LastSmrDate, options => options.MapFrom(s => s.LastSmrdate));
        }
    }
}