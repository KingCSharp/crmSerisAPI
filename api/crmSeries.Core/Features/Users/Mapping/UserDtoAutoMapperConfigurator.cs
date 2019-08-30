using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;

namespace crmSeries.Core.Features.Users.Mapping
{
    public class UserDtoAutoMapperConfigurator : Profile
    {
        public UserDtoAutoMapperConfigurator()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<GetUserDto, User>();
        }
    }
}
