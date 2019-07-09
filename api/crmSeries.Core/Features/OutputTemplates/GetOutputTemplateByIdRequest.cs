using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.OutputTemplates.Dtos;
using crmSeries.Core.Features.OutputTemplates.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.OutputTemplates
{
    [HeavyEquipmentContext]
    public class GetOutputTemplateByIdRequest : IRequest<GetOutputTemplateDto>
    {
        public GetOutputTemplateByIdRequest(int templateId)
        {
            TemplateId = templateId;
        }
        public int TemplateId { get; private set; }
    }

    public class GetOutputTemplateByIdHandler : IRequestHandler<GetOutputTemplateByIdRequest, GetOutputTemplateDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetOutputTemplateByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetOutputTemplateDto>> HandleAsync(GetOutputTemplateByIdRequest request)
        {
            var outputTemplate = _context.Set<OutputTemplate>()
                .ProjectTo<GetOutputTemplateDto>()
                .SingleOrDefault(x => x.TemplateId == request.TemplateId);

            if (outputTemplate == null)
                return Response<GetOutputTemplateDto>.ErrorAsync(OutputTemplatesConstants.ErrorMessages.OutputTemplateNotFound);

            return outputTemplate.AsResponseAsync();
        }
    }

    public class GetOutputTemplateByIdValidator : AbstractValidator<GetOutputTemplateByIdRequest>
    {
        public GetOutputTemplateByIdValidator()
        {
            RuleFor(x => x.TemplateId).GreaterThan(0);
        }
    }
}