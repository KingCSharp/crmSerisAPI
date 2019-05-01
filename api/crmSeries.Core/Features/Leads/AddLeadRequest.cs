using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Leads.Dtos;
using crmSeries.Core.Features.Leads.Validators;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Notifications.Email;
using FluentValidation;

namespace crmSeries.Core.Features.Leads
{
    [HeavyEquipmentContext]
    public class AddLeadRequest : AddLeadDto, IRequest<AddResponse>
    {
    }

    public class AddLeadRequestHandler : IRequestHandler<AddLeadRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IEmailNotifier _emailNotifier;

        public AddLeadRequestHandler(HeavyEquipmentContext context, IEmailNotifier emailNotifier)
        {
            _context = context;
            _emailNotifier = emailNotifier;
        }

        public Task<Response<AddResponse>> HandleAsync(AddLeadRequest request)
        {
            var lead = Mapper.Map<Lead>(request);

            _context.Set<Lead>().Add(lead);
            _context.SaveChanges();

            _emailNotifier.SendEmailAsync(GenerateEmail(request));

            return new AddResponse
                {
                    Id = lead.LeadId
                }
                .AsResponseAsync();
        }

        private EmailMessage GenerateEmail(AddLeadDto lead)
        {
            var body = new StringBuilder();
            body.Append($"Dear {Constants.Emails.Leads.DealerNameKey} Representative,<br/><br/>");
            body.Append("This email is to notify you that a new lead was submitted to your crmSeries:<br/><br/>");
            body.Append("<table>");
            body.Append(GetFieldInfo(nameof(lead.Title), lead.Title));
            body.Append(GetFieldInfo(nameof(lead.Name), lead.Name));
            body.Append(GetFieldInfo(nameof(lead.Phone), lead.Phone));
            body.Append(GetFieldInfo(nameof(lead.Email), lead.Email));
            body.Append(GetFieldInfo(nameof(lead.Cell), lead.Cell));
            body.Append(GetFieldInfo(nameof(lead.Fax), lead.Fax));
            body.Append(GetFieldInfo(nameof(lead.CompanyName), lead.CompanyName));
            body.Append(GetFieldInfo(nameof(lead.Department), lead.Department));
            body.Append(GetFieldInfo(nameof(lead.Position), lead.Position));
            body.Append(GetFieldInfo(nameof(lead.CompanyPhone), lead.CompanyPhone));
            body.Append(GetFieldInfo(nameof(lead.Web), lead.Web));
            body.Append(GetFieldInfo(nameof(lead.Address1), lead.Address1));
            body.Append(GetFieldInfo(nameof(lead.Address2), lead.Address2));
            body.Append(GetFieldInfo(nameof(lead.City), lead.City));
            body.Append(GetFieldInfo(nameof(lead.State), lead.State));
            body.Append(GetFieldInfo(nameof(lead.Zip), lead.Zip));
            body.Append(GetFieldInfo(nameof(lead.County), lead.County));
            body.Append(GetFieldInfo(nameof(lead.Country), lead.Country));
            body.Append(GetFieldInfo(nameof(lead.Comments), lead.Comments));
            body.Append(GetFieldInfo(nameof(lead.Description), lead.Description));
            body.Append("</table>");

            return new EmailMessage
            {
                Subject = "crmSeries - New Lead",
                Body = body.ToString()
            };
        }

        private string GetFieldInfo(string fieldName, string fieldValue)
        {
            if (!string.IsNullOrEmpty(fieldValue))
            {
                return "<tr><td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'><strong>" +
                       fieldName.SplitWords() +
                       "</strong></td><td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" +
                       fieldValue +
                       "</td></tr>";
            }

            return string.Empty;
        }
    }

    public class AddLeadValidator : AbstractValidator<AddLeadRequest>
    {
        public AddLeadValidator()
        {
            RuleFor(x => x)
                .SetValidator(new AddLeadDtoValidator());
        }
    }
}