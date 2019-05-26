using crmSeries.Core.Data;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;
using System.Collections.Generic;
using crmSeries.Core.Domain.HeavyEquipment;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace crmSeries.Core.Features.Notes
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class GetNotePurposesRequest : IRequest<IEnumerable<NotePurposeDto>>
    {
    }

    public class GetNotePurposesHandler : IRequestHandler<GetNotePurposesRequest, IEnumerable<NotePurposeDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetNotePurposesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<NotePurposeDto>>> HandleAsync(GetNotePurposesRequest request)
        {
            return _context.Set<NotePurpose>()
                .Where(x => !x.Deleted)
                .ProjectTo<NotePurposeDto>()
                .AsEnumerable()
                .AsResponseAsync();
        }
    }
}