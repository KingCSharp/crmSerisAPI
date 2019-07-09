using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using crmSeries.Core.Mediator;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.OutputTemplateFields.Dtos;
using System.Threading.Tasks;
using crmSeries.Core.Mediator.Attributes;

namespace crmSeries.Core.Features.OutputTemplateFields
{
    [DoNotValidate]
    [HeavyEquipmentContext]
    public class GetOutputTemplateFieldsRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetOutputTemplateFieldDto>>
    {
    }

    public class OutputTemplateFieldsHandler :
        IRequestHandler<GetOutputTemplateFieldsRequest, PagedQueryResult<GetOutputTemplateFieldDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public OutputTemplateFieldsHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetOutputTemplateFieldDto>>> HandleAsync(GetOutputTemplateFieldsRequest request)
        {
            var result = new PagedQueryResult<GetOutputTemplateFieldDto>();

            var outputTemplateFields = _context.Set<OutputTemplateField>()
                .AsQueryable();

            var count = outputTemplateFields.Count();

            result.PageCount = count / request.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            result.Items = outputTemplateFields.ProjectTo<GetOutputTemplateFieldDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }
}
