using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Mediator;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.OutputTemplates.Dtos;
using System.Threading.Tasks;
using crmSeries.Core.Mediator.Attributes;

namespace crmSeries.Core.Features.OutputTemplates
{
    [DoNotValidate]
    [HeavyEquipmentContext]
    public class GetOutputTemplatesRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetOutputTemplateDto>>
    {
        /// <summary>
        /// Optional filter for the unique identifier of the output template category for the returned templates.
        /// </summary>
        public int CategoryId { get; set; }
    }

    public class GetOutputTemplatesRequestHandler :
        IRequestHandler<GetOutputTemplatesRequest, PagedQueryResult<GetOutputTemplateDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetOutputTemplatesRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetOutputTemplateDto>>> HandleAsync(GetOutputTemplatesRequest request)
        {
            var result = new PagedQueryResult<GetOutputTemplateDto>();

            var outputTemplates = _context.Set<OutputTemplate>()
                .AsQueryable();

            if (request.CategoryId > 0)
                outputTemplates = outputTemplates.Where(x => x.CategoryId == request.CategoryId);

            var count = outputTemplates.Count();

            result.PageCount = count / request.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            result.Items = outputTemplates.ProjectTo<GetOutputTemplateDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }
}
