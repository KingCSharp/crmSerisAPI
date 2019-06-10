using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.CompanyRecordTypes.Dtos;

namespace crmSeries.Core.Features.CompanyRanks.Mapping
{
    public class CompanyRecordTypeDtoAutoMapperConfigurator : Profile
    {
        public CompanyRecordTypeDtoAutoMapperConfigurator()
        {
            CreateMap<CompanyRecordType, GetCompanyRecordTypeDto>();
            CreateMap<GetCompanyRecordTypeDto, CompanyRecordType>();
        }
    }
}
