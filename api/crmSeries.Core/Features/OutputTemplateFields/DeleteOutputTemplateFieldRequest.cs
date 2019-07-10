using System;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.OutputTemplateFields.Utility;

namespace crmSeries.Core.Features.OutputTemplateFields
{
    [HeavyEquipmentContext]
    public class DeleteOutputTemplateFieldRequest : IRequest
    {
        public DeleteOutputTemplateFieldRequest(int id)
        {
            FieldId = id;
        }
        public int FieldId { get; private set; }
    }

    public class DeleteOutputTemplateFieldHandler : IRequestHandler<DeleteOutputTemplateFieldRequest>
    {
        private readonly HeavyEquipmentContext _context;
        public DeleteOutputTemplateFieldHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(DeleteOutputTemplateFieldRequest request)
        {
            var outputTemplateField = _context.Set<OutputTemplateField>()
                .SingleOrDefault(x => x.FieldId == request.FieldId);

            if (outputTemplateField == null)
                return Response.ErrorAsync(OutputTemplateFieldsConstants.ErrorMessages.OutputTemplateFieldNotFound);

            _context.OutputTemplateField.Remove(outputTemplateField);
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    public class DeleteOutputTemplateFieldValidator : AbstractValidator<DeleteOutputTemplateFieldRequest>
    {
        public DeleteOutputTemplateFieldValidator()
        {
            RuleFor(x => x.FieldId).GreaterThan(0);
        }
    }
}