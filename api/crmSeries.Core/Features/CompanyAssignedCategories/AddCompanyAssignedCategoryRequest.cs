using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.CompanyAssignedCategories.Dtos;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.CompanyAssignedCategories
{
    [HeavyEquipmentContext]
    public class AddCompanyAssignedCategoryRequest : AddCompanyAssignedCategoryDto, IRequest<AddResponse>
    {
    }

    public class AddCompanyAssignedCategoryHandler : IRequestHandler<AddCompanyAssignedCategoryRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public AddCompanyAssignedCategoryHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddCompanyAssignedCategoryRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var companyAssignedCategory = request.MapTo<CompanyAssignedCategory>();

            _context.Set<CompanyAssignedCategory>().Add(companyAssignedCategory);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = companyAssignedCategory.AssignedId
            }.AsResponseAsync();
        }

        private bool IsValid(BaseCompanyAssignedCategoryDto request, out Task<Response<AddResponse>> errorAsync)
        {
            var relatedEntities = new List<(string, int)>
            {
                (Constants.RelatedRecord.Types.CompanyCategory, request.CategoryId),
                (Constants.RelatedRecord.Types.Company, request.CompanyId)
            };

            foreach (var (relatedRecordType, relatedRecordTypeId) in relatedEntities)
            {
                var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
                {
                    RecordType = relatedRecordType,
                    RecordTypeId = relatedRecordTypeId
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

    public class AddCompanyAssignedCategoryValidator : AbstractValidator<AddCompanyAssignedCategoryRequest>
    {
        public AddCompanyAssignedCategoryValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.CompanyId).GreaterThan(0);
        }
    }
}