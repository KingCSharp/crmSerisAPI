using System;
using crmSeries.Core.Features.DocuSign.Utility;
using FluentValidation;

namespace crmSeries.Core.Features.DocuSign.Dtos
{
    /// <summary>
    /// Contains the necessary information to identify a DocuSign template.
    /// </summary>
    public class TemplateDto
    {
        /// <summary>
        /// The template's DocuSign identifier (UUID)
        /// </summary>
        public string TemplateId { get; set; }
    }

    public class TemplateDtoValidator : AbstractValidator<TemplateDto>
    {
        public TemplateDtoValidator()
        {
            RuleFor(x => x.TemplateId)
                .NotEmpty();
        }
    }

    /// <summary>
    /// Contains the necessary information to identify a recipient of a DocuSign template.
    /// </summary>
    public class TemplateRecipientDto : DocuSignTemplateRecipient
    {
    }

    public class TemplateRecipientDtoValidator : AbstractValidator<TemplateRecipientDto>
    {
        public TemplateRecipientDtoValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email)
                        .Must(x => x.Split('@', StringSplitOptions.None).Length == 2)
                        .WithMessage("An invalid recipient email address was provided.");
                });
        }
    }

    /// <summary>
    /// Contains information about a DocuSign template.
    /// </summary>
    public class GetTemplateDto : DocuSignTemplate
    {
    }
}
