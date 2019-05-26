using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Notifications.Email;

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

            var emailTemplatePath = $@"{codeBase}{EmailTemplateConstants.Utility.TemplateDirectoryPath}";

            var emailTemplate = File.ReadAllText(emailTemplatePath);

            return emailTemplate.AsResponseAsync();
        }
    }
}
