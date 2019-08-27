using System.Linq;
using FluentValidation;
using crmSeries.Core.Mediator;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using System;
using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Validation;

namespace crmSeries.Core.Features.Notes
{
    [HeavyEquipmentContext]
    public class GetNotesRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetNoteDto>>
    {
        /// <summary>
        /// Setting this value will return all notes created with a timestamp
        /// great than or equal to this date.
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Setting this value will return all notes created with a timestamp
        /// less than or equal to this date.
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Setting this value will return all notes that have comments containing your search phrase.
        /// For obvious reasons, we are requiring the length of your search criteria to be at least
        /// 4 characters.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Setting this value will return all notes that belong to the specified contact.
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// Setting this value will return all notes that belong to the specified user.
        /// </summary>
        public int UserId { get; set; }
    }

    public class GetNotesRequestHandler :
        IRequestHandler<GetNotesRequest, PagedQueryResult<GetNoteDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;

        public GetNotesRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<GetNoteDto>>> HandleAsync(GetNotesRequest request)
        {
            var result = new PagedQueryResult<GetNoteDto>();

            var notes =
                (from n in _context.Set<Note>()
                    select new
                    {
                        n.NoteId,
                        n.UserId,
                        n.Comments,
                        n.RecordId,
                        n.RecordType,
                        n.NoteDate,
                        n.Latitude,
                        n.Longitude,
                    })
                .AsQueryable();

            if (request.FromDate != default)
                notes = notes.Where(x => x.NoteDate.ToUniversalTime() >= request.FromDate.ToUniversalTime());

            if (request.ToDate != default)
                notes = notes.Where(x => x.NoteDate.ToUniversalTime() <= request.ToDate.ToUniversalTime());

            if (!string.IsNullOrEmpty(request.Comments))
                notes = notes.Where(x => x.Comments.Contains(request.Comments));

            if (request.ContactId > 0)
                notes = notes.Where(x => x.RecordId == request.ContactId && x.RecordType == RelatedRecords.Constants.RelatedRecord.Types.Contact);

            if (request.UserId > 0)
                notes = notes.Where(x => x.UserId == request.UserId);

            var count = notes.Count();

            result.PageCount = count / request.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            result.Items = notes.ProjectTo<GetNoteDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }

    public class GetNotesValidator : AbstractValidator<GetNotesRequest>
    {
        public GetNotesValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x)
                .Must(HaveAFromDateLessThanOrEqualToTheToDate)
                .Unless(x => x.FromDate == default || x.ToDate == default)
                .WithMessage(Common.Constants.ErrorMessages.FromDateLessThanDate);

            RuleFor(x => x.FromDate)
                .SetValidator(new DateTimeDefaultValidator())
                .WithMessage(Common.Constants.ErrorMessages.InvalidDate)
                .Unless(x => x.FromDate == default);

            RuleFor(x => x.ToDate)
                .SetValidator(new DateTimeDefaultValidator())
                .WithMessage(Common.Constants.ErrorMessages.InvalidDate)
                .Unless(x => x.ToDate == default);

            RuleFor(x => x.Comments).MaximumLength(4);
        }

        private bool HaveAFromDateLessThanOrEqualToTheToDate(GetNotesRequest request)
        {
            return request.FromDate <= request.ToDate;
        }
    }
}
