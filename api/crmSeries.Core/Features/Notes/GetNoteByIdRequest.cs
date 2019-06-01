using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.Notes
{
    [HeavyEquipmentContext]
    public class GetNoteByIdRequest : IRequest<GetNoteDto>
    {
        public GetNoteByIdRequest(int noteId)
        {
            NoteId = noteId;
        }
        public int NoteId { get; private set; }
    }

    public class GetNoteByIdHandler : IRequestHandler<GetNoteByIdRequest, GetNoteDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetNoteByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetNoteDto>> HandleAsync(GetNoteByIdRequest request)
        {
            var note = (from t in _context.Set<Note>()
                    select new
                    {
                        t.NoteId,
                        t.UserId,
                        t.Comments,
                        t.Deleted,
                        t.RecordId,
                        t.RecordType,
                        t.NoteDate,
                        t.Latitude,
                        t.Longitude
                    })
                .ProjectTo<GetNoteDto>()
                .SingleOrDefault(x => x.NoteId == request.NoteId);

                if (note == null)
                    return Response<GetNoteDto>.ErrorAsync(NotesConstants.ErrorMessages.NoteNotFound);
                
                return note.AsResponseAsync();
        }
    }

    public class GetNoteByIdValidator : AbstractValidator<GetNoteByIdRequest>
    {
        public GetNoteByIdValidator()
        {
            RuleFor(x => x.NoteId).GreaterThan(0);
        }
    }
}