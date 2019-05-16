using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.CompanyAssignedAddresses.Dtos;
using crmSeries.Core.Features.Contacts.Dtos;
using System.Collections.Generic;

namespace crmSeries.Core.Features.Companies.Dtos
{
    public class CompanyFullDto
    {
        public CompanyDto Details { get; set; }
        public List<CompanyAssignedAddressDto> Addresses { get; set; }
        public List<ContactDto> Contacts { get; set; }
    }
}
