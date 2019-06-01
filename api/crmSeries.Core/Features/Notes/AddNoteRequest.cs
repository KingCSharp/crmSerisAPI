using System;
using System.Collections.Generic;
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

namespace crmSeries.Core.Features.Notes
{
    [HeavyEquipmentContext]
    public class AddNoteRequest : AddNoteDto, IRequest<AddResponse>
    {
    }

    public class AddNoteHandler : IRequestHandler<AddNoteRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public AddNoteHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddNoteRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var note = request.MapTo<Note>();
            note.LastModified = DateTime.UtcNow;

            _context.Set<Note>().Add(note);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = note.NoteId
            }.AsResponseAsync();
        }

        private bool IsValid(AddNoteRequest request, out Task<Response<AddResponse>> errorAsync)
        {
            var relatedEntities = new Dictionary<string, int>
            {
                { request.RecordType, request.RecordTypeId },
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
                    errorAsync = Response<AddResponse>.ErrorsAsync(result.Errors);
                    return false;
                }
            }

            errorAsync = null;
            return true;
        }
    }

    public class AddNoteValidator : AbstractValidator<AddNoteRequest>
    {
        public AddNoteValidator()
        {
            Include(new BaseNoteDtoValidator());
        }
    }
}