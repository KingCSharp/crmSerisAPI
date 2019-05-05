using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using System.Linq;
using System;

namespace crmSeries.Core.Features.Workflows
{
    [DoNotValidate]
    public class LeadEmailTemplateReplacementRequest : IRequest<string>
    {
        public int EntityId { get; set; }
        public string EmailTemplate { get; set; }
    }

    public class LeadEmailTemplateReplacementRequestHandler : IRequestHandler<LeadEmailTemplateReplacementRequest, string>
    {
        private readonly HeavyEquipmentContext _context;
        public LeadEmailTemplateReplacementRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<string>> HandleAsync(LeadEmailTemplateReplacementRequest request)
        {

            var lead = _context.Lead.Single(x => x.LeadId == request.EntityId);
            var leadStatus = _context.LeadStatus.SingleOrDefault(x => x.StatusId == lead.StatusId);
            var leadSource = _context.CompanySource.SingleOrDefault(x => x.SourceId == lead.SourceId);
            var leadOwner = _context.User.SingleOrDefault(x => x.UserId == lead.OwnerId);
            string emailTemplate = request.EmailTemplate;

            emailTemplate = emailTemplate.Replace("[(LeadDescription)]", lead.Description);
            emailTemplate = emailTemplate.Replace("[(LeadComments)]", lead.Comments);
            emailTemplate = emailTemplate.Replace("[(LeadStatus)]", leadStatus == null ? "" : leadStatus.Status);
            emailTemplate = emailTemplate.Replace("[(LeadSource)]", leadSource == null ? "" : leadSource.Source);
            emailTemplate = emailTemplate.Replace("[(LeadOwner)]", leadOwner == null ? "" : leadOwner.FirstName + " " + leadOwner.LastName);
            emailTemplate = emailTemplate.Replace("[(CompanyName)]", lead.CompanyName);
            emailTemplate = emailTemplate.Replace("[(Address1)]", lead.Address1);
            emailTemplate = emailTemplate.Replace("[(Address2)]", lead.Address2);
            emailTemplate = emailTemplate.Replace("[(City)]", lead.City);
            emailTemplate = emailTemplate.Replace("[(State)]", lead.State);
            emailTemplate = emailTemplate.Replace("[(Zip)]", lead.Zip);
            emailTemplate = emailTemplate.Replace("[(County)]", lead.County);
            emailTemplate = emailTemplate.Replace("[(Country)]", lead.Country);
            emailTemplate = emailTemplate.Replace("[(CompanyPhone)]", lead.Phone);
            emailTemplate = emailTemplate.Replace("[(FirstName)]", lead.FirstName);
            emailTemplate = emailTemplate.Replace("[(LastName)]", lead.LastName);
            emailTemplate = emailTemplate.Replace("[(Title)]", lead.Title);
            emailTemplate = emailTemplate.Replace("[(Position)]", lead.Position);
            emailTemplate = emailTemplate.Replace("[(Department)]", lead.Department);
            emailTemplate = emailTemplate.Replace("[(Email)]", lead.Email);

            if (lead.DateAssigned == null)
            {
                emailTemplate = emailTemplate.Replace("[(DateAssigned)]", "Not Assigned");
            }
            else
            {
                DateTimeOffset dt = lead.DateAssigned.Value;
                emailTemplate = emailTemplate.Replace("[(DateAssigned)]", dt.DateTime.ToShortDateString());
            }

            return emailTemplate.AsResponseAsync();
        }
    }
}
