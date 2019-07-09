using crmSeries.Core.Common;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Validation;
using FluentValidation;

namespace crmSeries.Core.Features.Notes.Validator
{
    public class BaseNoteDtoValidator : AbstractValidator<BaseNoteDto>
    {
        public BaseNoteDtoValidator()
        {
            RuleFor(x => x.RecordId).GreaterThan(0);
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.TypeId).GreaterThan(0);
            RuleFor(x => x.Comments).NotEmpty();

            RuleFor(x => x.Latitude).Must(BeAValidLatitude)
                .When(x => x.Latitude != default)
                .WithMessage(NotesConstants.ErrorMessages.InvalidLatitude);

            RuleFor(x => x.Longitude).Must(BeAValidLongitude)
                .When(x => x.Longitude != default)
                .WithMessage(NotesConstants.ErrorMessages.InvalidLongitude);

            RuleFor(x => x.NoteDate)
                .SetValidator(new DateTimeDefaultValidator())
                .WithMessage(Constants.ErrorMessages.InvalidDate);
        }

        private bool BeAValidLongitude(decimal longitude)
        {
            return longitude >= -180 && longitude <= 180;
        }

        private bool BeAValidLatitude(decimal latitude)
        {
            return latitude >= -90 && latitude <= 90;
        }
    }
}
