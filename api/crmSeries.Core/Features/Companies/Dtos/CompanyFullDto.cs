using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.CompanyAssignedAddresses.Dtos;
using crmSeries.Core.Features.Contacts.Dtos;
using System.Collections.Generic;

namespace crmSeries.Core.Features.Companies.Dtos
{
    public class CompanyFullDto
    {

        /// <summary>
        /// The company details.
        /// </summary>
        public CompanyDto Details { get; set; }

        /// <summary>
        /// The addresses for the company.
        /// </summary>
        public List<CompanyAssignedAddressDto> Addresses { get; set; }

        /// <summary>
        /// The contacts for the company.
        /// </summary>
        public List<GetContactDto> Contacts { get; set; }
    }
}
