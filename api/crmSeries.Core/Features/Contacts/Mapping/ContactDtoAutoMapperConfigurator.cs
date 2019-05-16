using AutoMapper;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts.Dtos;

namespace crmSeries.Core.Features.Companies.Mapping
{
    public class ContactDtoAutoMapperConfigurator : Profile
    {
        public ContactDtoAutoMapperConfigurator()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>();
        }
    }
}