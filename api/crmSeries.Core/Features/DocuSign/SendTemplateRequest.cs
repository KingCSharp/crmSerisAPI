using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Mediator;
using FluentValidation;

namespace crmSeries.Core.Features.DocuSign
{
    public class SendTemplateRequest : IRequest
    {
        /// <summary>
        /// The DocuSign template that will be sent to the recipients.
        /// </summary>
        public TemplateDto Template { get; set; }

        /// <summary>
        /// A list of recipients to whom the DocuSign template will be sent.
        /// Each recipient will be sent an email requesting his or her signature after the previous 
        /// signatory has completed his or her template role.
        /// </summary>
        public List<TemplateRecipientDto> Recipients { get; set; }
    }

    public class SendTemplateRequestHandler : IRequestHandler<SendTemplateRequest>
    {
        private readonly IDocuSignClient _docuSignClient;

        public SendTemplateRequestHandler(IDocuSignClient docuSignClient)
        {
            _docuSignClient = docuSignClient;
        }

        public async Task<Response> HandleAsync(SendTemplateRequest request)
        {
            try
            {
                await _docuSignClient.SendTemplate(request.Template.TemplateId, request.Recipients);
            }
            catch(Exception ex)
            {
                return Error.AsResponse($"Failed to send DocuSign template: {ex.Message}");
            }

            return Response.Success();
        }
    }

    public class SendTemplateRequestValidator : AbstractValidator<SendTemplateRequest>
    {
        public SendTemplateRequestValidator(IValidator<TemplateDto> templateValidator, IValidator<TemplateRecipientDto> recipientValidator)
        {
            RuleFor(x => x.Template)
                .NotNull()
                .DependentRules(() =>
                {
                    RuleFor(x => x.Template)
                        .SetValidator(templateValidator);
                });

            RuleFor(x => x.Recipients)
                .NotEmpty()
                .DependentRules(() =>
                { 
                    RuleForEach(x => x.Recipients)
                        .SetValidator(recipientValidator);
                });
        }
    }
}
