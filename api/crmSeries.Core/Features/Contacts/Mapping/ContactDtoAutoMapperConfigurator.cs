using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Features.Contacts.Dtos;

namespace crmSeries.Core.Features.Companies.Mapping
{
    public class ContactDtoAutoMapperConfigurator : Profile
    {
        public ContactDtoAutoMapperConfigurator()
        {
            CreateMap<Contact, GetContactDto>();
            CreateMap<GetContactDto, Contact>();

            CreateMap<AddContactRequest, Contact>()
                .ForMember(x => x.ContactId, options => options.Ignore())
                .ForMember(x => x.Deleted, options => options.Ignore())
                .ForMember(x => x.Active, options => options.Ignore())
                .ForMember(x => x.LastModified, options => options.Ignore());

            CreateMap<EditContactRequest, Contact>()
                .ForMember(x => x.LastModified, options => options.Ignore());
        }
    }
}