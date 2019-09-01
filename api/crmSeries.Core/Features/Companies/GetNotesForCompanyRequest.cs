using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetNotesForCompanyRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetNoteDto>>
    {
        public GetNotesForCompanyRequest(int companyId, int pageSize, int pageNumber)
        {
            CompanyId = companyId;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public int CompanyId { get; private set; }
    }

    public class GetNotesForCompanyHandler :
        IRequestHandler<GetNotesForCompanyRequest, PagedQueryResult<GetNoteDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public GetNotesForCompanyHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordHandler;
        }

        public Task<Response<PagedQueryResult<GetNoteDto>>> HandleAsync(GetNotesForCompanyRequest request)
        {
            var verificationRequest = new VerifyRelatedRecordRequest
            {
                RecordType = Constants.RelatedRecord.Types.Company,
                RecordTypeId = request.CompanyId
            };

            var verificationResult = _verifyRelatedRecordsHandler
                .HandleAsync(verificationRequest)
                .Result;

            if (verificationResult.HasErrors)
                return Response<PagedQueryResult<GetNoteDto>>.ErrorsAsync(verificationResult.Errors);

            var result = new PagedQueryResult<GetNoteDto>();

            var notes = _context.Set<Note>()
                .Where(x => (x.RecordType == Constants.RelatedRecord.Types.Contact && 
                    _context.Set<Contact>()
                        .Where(y => y.CompanyId == request.CompanyId)
                        .Select(y => y.ContactId)
                        .Contains(x.RecordId)) || (x.RecordId == request.CompanyId && x.RecordType == Constants.RelatedRecord.Types.Company)
                    );

            var count = notes.Count();

            result.PageCount = (count  + (request.PageSize - 1)) / request.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            result.Items = notes.ProjectTo<GetNoteDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }

    public class GetNotesForCompanyValidator : AbstractValidator<GetNotesForCompanyRequest>
    {
        public GetNotesForCompanyValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
        }
    }
}
