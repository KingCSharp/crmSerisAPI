using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.UserFavoriteRecords.Dtos;

namespace crmSeries.Core.Features.UserFavoriteRecords.Mapping
{
    public class UserFavoriteRecordDtoAutoMapperConfigurator : Profile
    {
        public UserFavoriteRecordDtoAutoMapperConfigurator()
        {
            CreateMap<UserFavoriteRecord, GetUserFavoriteRecordDto>();
            CreateMap<GetUserFavoriteRecordDto, UserFavoriteRecord>();

            CreateMap<UserFavoriteRecord, ToggleUserFavoriteRecordRequest>();
            CreateMap<ToggleUserFavoriteRecordRequest, UserFavoriteRecord>();

            CreateMap<UserFavoriteRecord, AddUserFavoriteRecordRequest>();
            CreateMap<AddUserFavoriteRecordRequest, UserFavoriteRecord>();
        }
    }
}
