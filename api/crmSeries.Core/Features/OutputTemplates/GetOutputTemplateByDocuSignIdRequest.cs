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
    public class GetOutputTemplateByDocuSignIdRequest : IRequest<GetOutputTemplateDto>
    {
        public GetOutputTemplateByDocuSignIdRequest(string docuSignTemplateId)
        {
            DocuSignTemplateId = docuSignTemplateId;
        }
        public string DocuSignTemplateId { get; private set; }
    }

    public class GetOutputTemplateByDocuSignIdHandler : IRequestHandler<GetOutputTemplateByDocuSignIdRequest, GetOutputTemplateDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetOutputTemplateByDocuSignIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetOutputTemplateDto>> HandleAsync(GetOutputTemplateByDocuSignIdRequest request)
        {
            var outputTemplate = _context.Set<OutputTemplate>()
                .ProjectTo<GetOutputTemplateDto>()
                .SingleOrDefault(x => x.SourceId == request.DocuSignTemplateId);

            if (outputTemplate == null)
                return Response<GetOutputTemplateDto>.ErrorAsync(OutputTemplatesConstants.ErrorMessages.DocuSignOutputTemplateNotFound);

            return outputTemplate.AsResponseAsync();
        }
    }

    public class GetOutputTemplateByDocuSignIdValidator : AbstractValidator<GetOutputTemplateByDocuSignIdRequest>
    {
        public GetOutputTemplateByDocuSignIdValidator()
        {
            RuleFor(x => x.DocuSignTemplateId)
                .NotEmpty();
        }
    }
}