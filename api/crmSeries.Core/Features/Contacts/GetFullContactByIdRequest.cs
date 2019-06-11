using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Features.Tasks.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Features.RelatedRecords;

namespace crmSeries.Core.Features.Contacts
{
    [HeavyEquipmentContext]
    public class GetFullContactByIdRequest : IRequest<GetFullContactDto>
    {
        public GetFullContactByIdRequest(int contactId)
        {
            ContactId = contactId;
        }
        public int ContactId { get; private set; }
    }

    public class GetFullContactByIdHandler : IRequestHandler<GetFullContactByIdRequest, GetFullContactDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetFullContactByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetFullContactDto>> HandleAsync(GetFullContactByIdRequest request)
        {
            var details = (from c in _context.Set<Contact>()
                    join company in _context.Set<Company>()
                        on c.CompanyId equals company.CompanyId
                    where c.ContactId == request.ContactId
                    select new
                    {
                        c.ContactId,
                        c.CompanyId,
                        c.FirstName,
                        c.MiddleName,
                        c.LastName,
                        c.NickName,
                        c.Phone,
                        c.Cell,
                        c.Fax,
                        c.Email,
                        c.Title,
                        c.Position,
                        c.Department,
                        c.Active,
                        c.LastModified,
                        company.CompanyName,
                        company.AccountNo
                    })
                .ProjectTo<GetContactDto>()
                .SingleOrDefault();

            if (details == null)
                return Response<GetFullContactDto>.ErrorAsync(ContactsConstants.ErrorMessages.ContactNotFound);

            var dto = new GetFullContactDto
            {
                Details = details,
                Tasks = GetRelatedTasks(request.ContactId), 
                Notes = GetRelatedNotes(request.ContactId)
            };

            return dto.AsResponseAsync();
        }

        private List<GetNoteDto> GetRelatedNotes(int contactId)
        {
            var relatedNoteRecordIds = _context.Set<Note>()
                .Where(x => x.RecordType == Constants.RelatedRecord.Types.Contact &&
                            x.RecordId == contactId)
                .Select(x => x.NoteId)
                .ToList();

            if (!relatedNoteRecordIds.Any())
                return new List<GetNoteDto>();

            return _context.Set<Note>()
                .Where(x => relatedNoteRecordIds.Contains(x.NoteId))
                .ProjectTo<GetNoteDto>()
                .ToList();
        }

        private List<GetTaskDto> GetRelatedTasks(int contactId)
        {
            return _context
                .Set<Domain.HeavyEquipment.Task>()
                .Where(x => x.ContactId == contactId)
                .ProjectTo<GetTaskDto>()
                .ToList();
        }
    }

    public class GetFullContactByIdValidator : AbstractValidator<GetFullContactByIdRequest>
    {
        public GetFullContactByIdValidator()
        {
            RuleFor(x => x.ContactId).GreaterThan(0);
        }
    }
}
