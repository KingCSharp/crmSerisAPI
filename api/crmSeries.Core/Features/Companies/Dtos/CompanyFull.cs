using crmSeries.Core.Domain.HeavyEquipment;
using System.Collections.Generic;

namespace crmSeries.Core.Features.Companies.Dtos
{
    public class CompanyFull
    {
        public Company Details { get; set; }
        public List<CompanyAssignedAddress> Addresses { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
