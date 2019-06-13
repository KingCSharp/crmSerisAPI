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
using System.Diagnostics;

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
            var contactDto =
            (from c in _context.Set<Contact>()
             join company in _context.Set<Company>()
                 on c.CompanyId equals company.CompanyId
             where c.Active && !c.Deleted && c.ContactId == request.ContactId
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
             .GroupJoin(
                _context.Task,
                contact => contact.ContactId,
                task => task.ContactId,
                (x, y) => new
                {
                    Details = x,
                    Tasks = y.Where(t => !t.Deleted)
                }
            )
            .GroupJoin(
                _context.Note,
                contact => contact.Details.ContactId,
                notes => notes.RecordId,
                (x, y) => new
                {
                    x.Details,
                    x.Tasks,
                    Notes = y.Where(n => !n.Deleted && n.RecordType == Constants.RelatedRecord.Types.Contact)
                }
            )
            .ProjectTo<GetFullContactDto>()
            .SingleOrDefault();

            if (contactDto == null)
                return Response<GetFullContactDto>.ErrorAsync(ContactsConstants.ErrorMessages.ContactNotFound);

            return contactDto.AsResponseAsync();
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
