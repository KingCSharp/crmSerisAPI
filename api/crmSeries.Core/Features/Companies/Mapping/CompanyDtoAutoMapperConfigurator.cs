using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Features.Notes.Dtos;

namespace crmSeries.Core.Features.Companies.Mapping
{
    public class CompanyDtoAutoMapperConfigurator : Profile
    {
        public CompanyDtoAutoMapperConfigurator()
        {
            CreateMap<Company, GetCompanyDto>();
            CreateMap<GetCompanyDto, Company>();

            CreateMap<GetNotesForCompanyRequest, GetNoteDto>();

            CreateMap<AddCompanyRequest, Company>()
                .ForMember(x => x.CompanyId, options => options.Ignore())
                .ForMember(x => x.Deleted, options => options.Ignore())
                .ForMember(x => x.LastModified, options => options.Ignore());

            CreateMap<EditCompanyRequest, Company>()
                .ForMember(x => x.LastModified, options => options.Ignore());
        }
    }
}