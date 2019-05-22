using System;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Features.Contacts.Validator;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class AddCompanyRequest : AddCompanyDto, IRequest<AddResponse>
    {
    }

    public class AddCompanyHandler : IRequestHandler<AddCompanyRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;

        public AddCompanyHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<AddResponse>> HandleAsync(AddCompanyRequest request)
        {
            var company = request.MapTo<Company>();
            company.LastModified = DateTime.UtcNow;

            _context.Set<Company>().Add(company);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = company.CompanyId
            }.AsResponseAsync();
        }
    }

    public class AddCompanyValidator : AbstractValidator<AddCompanyRequest>
    {
        public AddCompanyValidator()
        {
            Include(new BaseCompanyDtoValidator());
        }
    }
}