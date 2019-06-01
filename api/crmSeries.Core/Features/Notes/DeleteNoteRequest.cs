using System;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Notes.Utility;

namespace crmSeries.Core.Features.Notes
{
    [HeavyEquipmentContext]
    public class DeleteNoteRequest : IRequest 
    {
        public DeleteNoteRequest(int id)
        {
            NoteId = id;
        }
        public int NoteId { get; private set; }
    }

    public class DeleteNoteHandler : IRequestHandler<DeleteNoteRequest>
    {
        private readonly HeavyEquipmentContext _context;
        public DeleteNoteHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(DeleteNoteRequest request)
        {
            var task = _context.Set<Note>()
                .SingleOrDefault(x => x.NoteId == request.NoteId);

            if (task == null)
                return Response.ErrorAsync(NotesConstants.ErrorMessages.NoteNotFound);

            task.LastModified = DateTime.UtcNow;
            task.Deleted = true;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }
    
    public class DeleteNoteValidator : AbstractValidator<DeleteNoteRequest>
    {
        public DeleteNoteValidator()
        {
            RuleFor(x => x.NoteId).GreaterThan(0);
        }
    }
}