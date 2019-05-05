using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;

namespace crmSeries.Core.Features.Notifications
{
    [DoNotValidate]
    public class GetEmailTemplateRequest : IRequest<string>
    {
    }

    public class GetEmailTemplateRequestHandler : IRequestHandler<GetEmailTemplateRequest, string>
    {
        public Task<Response<string>> HandleAsync(GetEmailTemplateRequest request)
        {
            var codeBase = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            // TODO - Get rid of the magic string.
            var emailTemplatePath = $@"{codeBase}\Data\Templates\standard-email-template.html";

            var emailTemplate = File.ReadAllText(emailTemplatePath);

            return emailTemplate.AsResponseAsync();
        }
    }
}
