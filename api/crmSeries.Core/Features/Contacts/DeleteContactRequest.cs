using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using crmSeries.Core.Features.Leads.Utility;

namespace crmSeries.Core.Features.Contacts
{
    [HeavyEquipmentContext]
    public class DeleteContactRequest : IRequest 
    {
        public DeleteContactRequest(int id)
        {
            ContactId = id;
        }
        public int ContactId { get; private set; }
    }

    public class DeleteContactHandler : IRequestHandler<DeleteContactRequest>
    {
        private readonly HeavyEquipmentContext _context;
        public DeleteContactHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(DeleteContactRequest request)
        {
            var contact = _context.Contact.SingleOrDefault(x => x.ContactId == request.ContactId);

            if (contact == null)
                return Response.ErrorAsync(ContactsConstants.ErrorMessages.ContactNotFound);

            contact.Active = false;
            contact.Deleted = true;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    
    public class DeleteContactValidator : AbstractValidator<DeleteContactRequest>
    {
        public DeleteContactValidator()
        {
            RuleFor(x => x.ContactId).GreaterThan(0);
        }
    }
}