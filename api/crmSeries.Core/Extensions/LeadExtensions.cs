using crmSeries.Core.Domain.HeavyEquipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Extensions
{
    public static class LeadExtensions
    {
        public static Lead SetDefaults(this Lead lead)
        {
            lead.OwnerId = 0;
            lead.CompanyId = 0;
            lead.ContactId = 0;

            lead.Deleted = false;

            lead.DateAssigned = null;
            lead.DateAcknowledged = null;
            lead.DateConverted = null;

            lead.Description = lead.Description ?? "";
            lead.Comments = lead.Comments ?? "";

            lead.CompanyName = lead.CompanyName ?? "";
            lead.Address1 = lead.Address1 ?? "";
            lead.Address2 = lead.Address2 ?? "";
            lead.City = lead.City ?? "";
            lead.State = lead.State ?? "";
            lead.Zip = lead.Zip ?? "";
            lead.County = lead.County ?? "";
            lead.Country = lead.Country ?? "";
            lead.CompanyPhone = lead.CompanyPhone ?? "";
            lead.Web = lead.Web ?? "";
            lead.Fax = lead.Fax ?? "";

            lead.FirstName = lead.FirstName ?? "";
            lead.LastName = lead.LastName ?? "";
            lead.Email = lead.Email ?? "";
            lead.Phone = lead.Phone ?? "";
            lead.Cell = lead.Cell ?? "";
            lead.Title = lead.Title ?? "";
            lead.Position = lead.Position ?? "";
            lead.Department = lead.Department ?? "";

            return lead;
        }
    }
}
