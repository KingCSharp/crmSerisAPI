using System;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.OutputTemplates.Utility;

namespace crmSeries.Core.Features.OutputTemplates
{
    [HeavyEquipmentContext]
    public class DeleteOutputTemplateRequest : IRequest
    {
        public DeleteOutputTemplateRequest(int id)
        {
            TemplateId = id;
        }
        public int TemplateId { get; private set; }
    }

    public class DeleteOutputTemplateHandler : IRequestHandler<DeleteOutputTemplateRequest>
    {
        private readonly HeavyEquipmentContext _context;
        public DeleteOutputTemplateHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(DeleteOutputTemplateRequest request)
        {
            var outputTemplate = _context.Set<OutputTemplate>()
                .SingleOrDefault(x => x.TemplateId == request.TemplateId);

            if (outputTemplate == null)
                return Response.ErrorAsync(OutputTemplatesConstants.ErrorMessages.OutputTemplateNotFound);

            _context.OutputTemplate.Remove(outputTemplate);
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    public class DeleteOutputTemplateValidator : AbstractValidator<DeleteOutputTemplateRequest>
    {
        public DeleteOutputTemplateValidator()
        {
            RuleFor(x => x.TemplateId).GreaterThan(0);
        }
    }
}