using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.CompanyAssignedCategories;
using crmSeries.Core.Features.CompanyAssignedRanks;
using crmSeries.Core.Features.CompanyAssignedRanks.Dtos;
using crmSeries.Core.Features.Contacts.Validator;
using crmSeries.Core.Features.RelatedRecords;
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
        private readonly IRequestHandler<AddCompanyAssignedCategoryRequest, AddResponse> _addCompanyAssignedCategoryHandler;
        private readonly IRequestHandler<DeleteCompanyAssignedCategoryRequest> _deleteCompanyAssignedCategoryHandler;
        private readonly IRequestHandler<AddCompanyAssignedRankRequest, AddResponse> _addCompanyAssignedRankHandler;
        private readonly IRequestHandler<DeleteCompanyAssignedRankRequest> _deleteCompanyAssignedRankHandler;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public EditCompanyHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler,
            IRequestHandler<AddCompanyAssignedCategoryRequest, AddResponse> addCompanyAssignedCategoryHandler,
            IRequestHandler<DeleteCompanyAssignedCategoryRequest> deleteCompanyAssignedCategoryHandler,
            IRequestHandler<AddCompanyAssignedRankRequest, AddResponse> addCompanyAssignedRankHandler,
            IRequestHandler<DeleteCompanyAssignedRankRequest> deleteCompanyAssignedRankHandler)
        {
            _context = context;
            _addCompanyAssignedCategoryHandler = addCompanyAssignedCategoryHandler;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
            _deleteCompanyAssignedCategoryHandler = deleteCompanyAssignedCategoryHandler;
            _addCompanyAssignedRankHandler = addCompanyAssignedRankHandler;
            _deleteCompanyAssignedRankHandler = deleteCompanyAssignedRankHandler;
        }

        public Task<Response> HandleAsync(EditCompanyRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var companyEntity = request.MapTo<Company>();
            companyEntity.LastModified = DateTime.UtcNow;

            _context.Set<Company>().Attach(companyEntity);
            _context.Entry(companyEntity).State = EntityState.Modified;
            _context.SaveChanges();

            HandleAssignedCategories(request.Categories, request.CompanyId);
            HandleAssignedRanks(request.Ranks, request.CompanyId);

            return Response.SuccessAsync();
        }

        private bool IsValid(EditCompanyRequest request, out Task<Response> errorAsync)
        {
            var relatedEntities = new List<(string, int)>
            {
                (Constants.RelatedRecord.Types.Company, request.CompanyId)
            };

            request.Categories.ForEach(x =>
            {
                relatedEntities.Add((Constants.RelatedRecord.Types.CompanyCategory, x));
            });

            request.Ranks.ForEach(x =>
            {
                relatedEntities.Add((Constants.RelatedRecord.Types.Rank, x.RankId));
                relatedEntities.Add((Constants.RelatedRecord.Types.UserRole, x.RoleId));
            });


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
                    errorAsync = Response.ErrorsAsync(result.Errors);
                    return false;
                }
            }

            errorAsync = null;
            return true;
        }

        private void HandleAssignedCategories(List<int> categories, int companyId)
        {
            if (categories == null)
                return;

            categories = categories.Distinct().ToList();
            categories.RemoveAll(x => x == 0);

            var existingCategories = _context.CompanyAssignedCategory
                .Where(x => x.CompanyId == companyId);

            existingCategories
                .Where(x => !categories.Contains(x.CategoryId))
                .ForEach(x => 
                {
                    _deleteCompanyAssignedCategoryHandler
                    .HandleAsync(new DeleteCompanyAssignedCategoryRequest(x.AssignedId));
                });

            existingCategories
                .Where(x => categories.Contains(x.CategoryId))
                .ForEach(x => { categories.Remove(x.CategoryId); });

            categories.ForEach(x =>
            {
                _addCompanyAssignedCategoryHandler
                .HandleAsync(new AddCompanyAssignedCategoryRequest
                {
                    CategoryId = x,
                    CompanyId = companyId
                });
            });
        }

        private void HandleAssignedRanks(List<AddCompanyAssignedRankDto> ranks, int companyId)
        {
            if (ranks == null)
                return;

            ranks = ranks.Distinct().ToList();
            ranks.RemoveAll(x => x.RankId == 0 || x.RoleId == 0);

            var existingRanks = _context.CompanyAssignedRank
                .Where(x => x.CompanyId == companyId);

            existingRanks
                .Where(x => !ranks.Any(y => y.RankId == x.RankId && y.RoleId == x.RoleId))
                .ForEach(x =>
                {
                    _deleteCompanyAssignedRankHandler
                    .HandleAsync(new DeleteCompanyAssignedRankRequest(x.AssignedId));
                });

            existingRanks
                .Where(x => ranks.Any(y => y.RankId == x.RankId && y.RoleId == x.RoleId))
                .ForEach(x => 
                {
                    ranks.RemoveAll(y => y.RankId == x.RankId && y.RoleId == x.RoleId);
                });

            ranks.ForEach(x =>
            {
                _addCompanyAssignedRankHandler
                .HandleAsync(new AddCompanyAssignedRankRequest
                {
                    CompanyId = companyId,
                    RankId = x.RankId,
                    RoleId = x.RoleId
                });
            });
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