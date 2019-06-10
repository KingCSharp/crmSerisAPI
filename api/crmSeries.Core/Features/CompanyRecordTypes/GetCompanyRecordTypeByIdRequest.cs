using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Features.CompanyRecordTypes.Dtos;
using crmSeries.Core.Features.CompanyRecordTypes.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.CompanyRecordTypes
{
    [HeavyEquipmentContext]
    public class GetCompanyRecordTypeByIdRequest : IRequest<GetCompanyRecordTypeDto>
    {
        public GetCompanyRecordTypeByIdRequest(int companyRecordTypeId)
        {
            CompanyRecordTypeId = companyRecordTypeId;
        }
        public int CompanyRecordTypeId { get; private set; }
    }

    public class GetCompanyRecordTypeByIdHandler : IRequestHandler<GetCompanyRecordTypeByIdRequest, GetCompanyRecordTypeDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetCompanyRecordTypeByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetCompanyRecordTypeDto>> HandleAsync(GetCompanyRecordTypeByIdRequest request)
        {
            var companyRecordType = _context.CompanyRecordType
                .ProjectTo<GetCompanyRecordTypeDto>()
                .SingleOrDefault(x => x.TypeId == request.CompanyRecordTypeId && !x.Deleted);

            if (companyRecordType == null)
                return Response<GetCompanyRecordTypeDto>
                    .ErrorAsync(CompanyRecordTypesConstants.ErrorMessages.CompanyRecordTypeNotFound);

            return companyRecordType.AsResponseAsync();
        }
    }

    public class GetCompanyRecordTypeByIdValidator : AbstractValidator<GetCompanyRecordTypeByIdRequest>
    {
        public GetCompanyRecordTypeByIdValidator()
        {
            RuleFor(x => x.CompanyRecordTypeId).GreaterThan(0);
        }
    }
}