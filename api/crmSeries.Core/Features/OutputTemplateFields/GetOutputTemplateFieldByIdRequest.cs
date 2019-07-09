using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.OutputTemplateFields.Dtos;
using crmSeries.Core.Features.OutputTemplateFields.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.OutputTemplateFields
{
    [HeavyEquipmentContext]
    public class GetOutputTemplateFieldByIdRequest : IRequest<GetOutputTemplateFieldDto>
    {
        public GetOutputTemplateFieldByIdRequest(int fieldId)
        {
            FieldId = fieldId;
        }
        public int FieldId { get; private set; }
    }

    public class GetOutputTemplateFieldByIdHandler : IRequestHandler<GetOutputTemplateFieldByIdRequest, GetOutputTemplateFieldDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetOutputTemplateFieldByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetOutputTemplateFieldDto>> HandleAsync(GetOutputTemplateFieldByIdRequest request)
        {
            var outputTemplateField = _context.Set<OutputTemplateField>()
                .ProjectTo<GetOutputTemplateFieldDto>()
                .SingleOrDefault(x => x.FieldId == request.FieldId);

            if (outputTemplateField == null)
                return Response<GetOutputTemplateFieldDto>.ErrorAsync(OutputTemplateFieldsConstants.ErrorMessages.OutputTemplateFieldNotFound);

            return outputTemplateField.AsResponseAsync();
        }
    }

    public class GetOutputTemplateFieldByIdValidator : AbstractValidator<GetOutputTemplateFieldByIdRequest>
    {
        public GetOutputTemplateFieldByIdValidator()
        {
            RuleFor(x => x.FieldId).GreaterThan(0);
        }
    }
}