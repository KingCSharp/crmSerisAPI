using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Features.Notes.Validator;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Users.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.Notes
{
    [HeavyEquipmentContext]
    public class EditNoteRequest : EditNoteDto, IRequest
    {
    }

    public class EditNoteHandler : IRequestHandler<EditNoteRequest>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public EditNoteHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }
        
        public Task<Response> HandleAsync(EditNoteRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var NoteEntity = request.MapTo<Note>();
            NoteEntity.LastModified = DateTime.UtcNow;
            
            _context.Set<Note>().Attach(NoteEntity);
            _context.Entry(NoteEntity).State = EntityState.Modified;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }

        private bool IsValid(EditNoteRequest request, out Task<Response> errorAsync)
        {
            var relatedEntities = new Dictionary<string, int>
            {
                { request.RecordType, request.RecordTypeId },
                { Constants.RelatedRecord.Types.Note, request.NoteId },
                { Constants.RelatedRecord.Types.User, request.UserId },
            };

            foreach (var entity in relatedEntities)
            {
                var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
                {
                    RecordType = entity.Key,
                    RecordTypeId = entity.Value
                };

                var result = _verifyRelatedRecordsHandler.HandleAsync(verifyRelatedRecordRequest).Result;

                if (result.HasErrors)
                {
                    errorAsync = Response.ErrorsAsync(result.Errors);
                    return false;
                }
            }

            errorAsync = null;
            return true;
        }
    }

    public class EditNoteValidator : AbstractValidator<EditNoteRequest>
    {
        public EditNoteValidator()
        {
            RuleFor(x => x.NoteId).GreaterThan(0);
            Include(new BaseNoteDtoValidator());
        }
    }
}