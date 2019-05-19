using System;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Features.Contacts.Validator;
using crmSeries.Core.Features.Leads.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.Contacts
{
    [HeavyEquipmentContext]
    public class EditContactRequest : EditContactDto, IRequest
    {
    }

    public class EditContactHandler : IRequestHandler<EditContactRequest>
    {
        private readonly HeavyEquipmentContext _context;

        public EditContactHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }
        
        public Task<Response> HandleAsync(EditContactRequest request)
        {
            var contactExists = _context.Set<Contact>().Any(x => x.ContactId == request.ContactId);
            if (!contactExists)
                return Response.ErrorAsync(ContactsConstants.ErrorMessages.ContactNotFound);

            var exists = _context.Set<Company>().Any(x => x.CompanyId == request.CompanyId);
            if (!exists)
            {
                return Response.ErrorAsync(CompaniesConstants.ErrorMessages.CompanyIdNotValid);
            }
            
            var contactEntity = request.MapTo<Contact>();
            contactEntity.LastModified = DateTime.UtcNow;
            
            _context.Set<Contact>().Attach(contactEntity);
            _context.Entry(contactEntity).State = EntityState.Modified;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    public class EditContactValidator : AbstractValidator<EditContactRequest>
    {
        public EditContactValidator()
        {
            RuleFor(x => x.ContactId).GreaterThan(0);
            Include(new BaseContactDtoValidator());
        }
    }
}