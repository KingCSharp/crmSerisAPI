using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using FluentValidation;
using crmSeries.Core.Mediator;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Notes.Dtos;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Notes
{
    [HeavyEquipmentContext]
    public class GetNotesRequest : IRequest<PagedQueryResult<GetNoteDto>>
    {
        public PagedQueryRequest PageInfo { get; set; }
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
                    join u in _context.Set<User>()
                        on n.UserId equals u.UserId
                    where u.UserId == _identity.RequestingUser.UserId
                    select new
                    {
                        n.NoteId,
                        n.UserId,
                        n.Comments,
                        n.Deleted,
                        n.RecordId,
                        n.RecordType,
                        n.NoteDate,
                        n.Latitude,
                        n.Longitude,
                    })
                .AsQueryable();

            var count = notes.Count();

            result.PageCount = count / request.PageInfo.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageInfo.PageNumber;
            result.PageSize = request.PageInfo.PageSize;

            result.Items = notes.ProjectTo<GetNoteDto>()
                .GetPagedData(request.PageInfo)
                .ToList();

            return result.AsResponseAsync();
        }
    }

    public class GetNotesValidator : AbstractValidator<GetNotesRequest>
    {
        public GetNotesValidator()
        {
            RuleFor(x => x.PageInfo.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageInfo.PageSize)
                .GreaterThan(0);
        }
    }
}
