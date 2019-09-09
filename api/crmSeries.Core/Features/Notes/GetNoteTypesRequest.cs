using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Features.Notes
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class GetNoteTypesRequest : IRequest<IEnumerable<NoteTypeDto>>
    {
    }

    public class GetNoteTypesHandler : IRequestHandler<GetNoteTypesRequest, IEnumerable<NoteTypeDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetNoteTypesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<NoteTypeDto>>> HandleAsync(GetNoteTypesRequest request)
        {
            return _context.Set<NoteType>()
                .Where(x => !x.Deleted)
                .ProjectTo<NoteTypeDto>()
                .AsEnumerable()
                .AsResponseAsync();
        }
    }
}