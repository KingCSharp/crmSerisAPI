using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Features.Notes
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class GetNoteTypesRequest : IRequest<IEnumerable<string>>
    {
    }

    public class GetNoteTypesHandler : IRequestHandler<GetNoteTypesRequest, IEnumerable<string>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetNoteTypesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<string>>> HandleAsync(GetNoteTypesRequest request)
        {
            return _context.Set<NoteType>()
                .Where(x => !x.Deleted)
                .Select(x => x.Type)
                .AsEnumerable()
                .AsResponseAsync();
        }
    }
}