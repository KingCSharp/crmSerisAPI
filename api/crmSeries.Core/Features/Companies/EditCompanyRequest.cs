using System;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.Contacts.Validator;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class EditCompanyRequest : EditCompanyDto, IRequest
    {
    }

    public class EditCompanyHandler : IRequestHandler<EditCompanyRequest>
    {
        private readonly HeavyEquipmentContext _context;

        public EditCompanyHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(EditCompanyRequest request)
        {
            var companyExists = _context.Set<Company>().Any(x => x.CompanyId == request.CompanyId);
            if (!companyExists)
                return Response.ErrorAsync(CompaniesConstants.ErrorMessages.CompanyNotFound);

            var companyEntity = request.MapTo<Company>();
            companyEntity.LastModified = DateTime.UtcNow;

            _context.Set<Company>().Attach(companyEntity);
            _context.Entry(companyEntity).State = EntityState.Modified;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    public class EditCompanyValidator : AbstractValidator<EditCompanyRequest>
    {
        public EditCompanyValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
            Include(new BaseCompanyDtoValidator());
        }
    }
}