using crmSeries.Core.Features.Inspections.Dtos;
using FluentValidation;

namespace crmSeries.Core.Features.Inspections.Validator
{
    public class SaveInspectionDtoValidator 
        : AbstractValidator<SaveInspectionDto>
    {
        public SaveInspectionDtoValidator(IValidator<RecordAssignedInspectionDto> inspectionValidator)
        {
            When(x => x.AssignedInspectionId.HasValue,
                    () => RuleFor(x => x.AssignedInspectionId).GreaterThan(0));

            RuleFor(x => x.Inspection)
                .NotNull()
                .SetValidator(inspectionValidator);
        }
    }
    
    public class BaseRecordAssignedInspectionDtoValidator 
        : AbstractValidator<BaseRecordAssignedInspectionDto>
    {
        public BaseRecordAssignedInspectionDtoValidator()
        {
            RuleFor(x => x.InspectionId)
                .GreaterThan(0);

            RuleFor(x => x.RecordType)
                .NotEmpty();

            RuleFor(x => x.RecordId)
                .GreaterThan(0);

            RuleFor(x => x.InspectionName)
                .NotEmpty();

            RuleFor(x => x.InspectionType)
                .NotEmpty();

            RuleFor(x => x.InspectionHours)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Comments)
                .NotNull();
        }
    }

    public class BaseRecordAssignedInspectionGroupDtoValidator 
        : AbstractValidator<BaseRecordAssignedInspectionGroupDto>
    {
        public BaseRecordAssignedInspectionGroupDtoValidator()
        {
            RuleFor(x => x.GroupName)
                .NotEmpty();

            RuleFor(x => x.Sequence)
                .GreaterThanOrEqualTo(0);
        }
    }

    public class BaseRecordAssignedInspectionItemDtoValidator 
        : AbstractValidator<BaseRecordAssignedInspectionItemDto>
    {
        public BaseRecordAssignedInspectionItemDtoValidator()
        {
            RuleFor(x => x.Item)
                .NotEmpty();

            RuleFor(x => x.Sequence)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.DataType)
                .NotEmpty();

            RuleFor(x => x.Response)
                .NotNull();

            RuleFor(x => x.Comments)
                .NotNull();
        }
    }

    public class BaseRecordAssignedInspectionItemResponseDtoValidator 
        : AbstractValidator<BaseRecordAssignedInspectionItemResponseDto>
    {
        public BaseRecordAssignedInspectionItemResponseDtoValidator()
        {
            RuleFor(x => x.Response)
                .NotEmpty();

            RuleFor(x => x.Sequence)
                .GreaterThanOrEqualTo(0);
        }
    }

    public class RecordAssignedInspectionDtoValidator 
        : AbstractValidator<RecordAssignedInspectionDto>
    {
        public RecordAssignedInspectionDtoValidator(IValidator<BaseRecordAssignedInspectionDto> baseValidator,
            IValidator<RecordAssignedInspectionGroupDto> groupValidator)
        {
            Include(baseValidator);

            RuleFor(x => x.Groups)
                .NotNull();

            When(x => x.Groups != null, () => RuleForEach(x => x.Groups).SetValidator(groupValidator));
        }
    }

    public class RecordAssignedInspectionGroupDtoValidator
        : AbstractValidator<RecordAssignedInspectionGroupDto>
    {
        public RecordAssignedInspectionGroupDtoValidator(IValidator<BaseRecordAssignedInspectionGroupDto> baseValidator,
            IValidator<RecordAssignedInspectionItemDto> itemValidator)
        {
            Include(baseValidator);
            
            RuleFor(x => x.Items)
                .NotNull();

            When(x => x.Items != null, () => RuleForEach(x => x.Items).SetValidator(itemValidator));
        }
    }

    public class RecordAssignedInspectionItemDtoValidator
        : AbstractValidator<RecordAssignedInspectionItemDto>
    {
        public RecordAssignedInspectionItemDtoValidator(IValidator<BaseRecordAssignedInspectionItemDto> baseValidator,
            IValidator<RecordAssignedInspectionItemResponseDto> itemResponseValidator)
        {
            Include(baseValidator);

            RuleFor(x => x.Responses)
                .NotNull();

            When(x => x.Responses != null, () => RuleForEach(x => x.Responses).SetValidator(itemResponseValidator));
        }
    }

    public class RecordAssignedInspectionItemResponseDtoValidator
        : AbstractValidator<RecordAssignedInspectionItemResponseDto>
    {
        public RecordAssignedInspectionItemResponseDtoValidator(IValidator<BaseRecordAssignedInspectionItemResponseDto> baseValidator)
        {
            Include(baseValidator);
        }
    }
}
