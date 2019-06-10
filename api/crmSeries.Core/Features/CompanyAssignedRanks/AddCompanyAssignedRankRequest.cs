using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.CompanyAssignedRanks.Dtos;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.CompanyAssignedRanks
{
    [HeavyEquipmentContext]
    public class AddCompanyAssignedRankRequest : AddCompanyAssignedRankDto, IRequest<AddResponse>
    {
    }

    public class AddCompanyAssignedRankHandler : IRequestHandler<AddCompanyAssignedRankRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public AddCompanyAssignedRankHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddCompanyAssignedRankRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var companyAssignedRank = request.MapTo<CompanyAssignedRank>();

            _context.Set<CompanyAssignedRank>().Add(companyAssignedRank);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = companyAssignedRank.AssignedId
            }.AsResponseAsync();
        }

        private bool IsValid(AddCompanyAssignedRankDto request, out Task<Response<AddResponse>> errorAsync)
        {
            var relatedEntities = new List<(string, int)>
            {
                (Constants.RelatedRecord.Types.Rank, request.RankId),
                (Constants.RelatedRecord.Types.Company, request.CompanyId),
                (Constants.RelatedRecord.Types.UserRole, request.RoleId)
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

    public class AddCompanyAssignedRankValidator : AbstractValidator<AddCompanyAssignedRankRequest>
    {
        public AddCompanyAssignedRankValidator()
        {
            RuleFor(x => x.RoleId).GreaterThan(0);
            RuleFor(x => x.RankId).GreaterThan(0);
            RuleFor(x => x.CompanyId).GreaterThan(0);
        }
    }
}