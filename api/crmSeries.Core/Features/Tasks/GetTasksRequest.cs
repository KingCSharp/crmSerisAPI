using System;
using System.Collections.Generic;
using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using FluentValidation;
using crmSeries.Core.Mediator;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Tasks.Dtos;
using Task = crmSeries.Core.Domain.HeavyEquipment.Task;
using static crmSeries.Core.Features.RelatedRecords.Constants;

namespace crmSeries.Core.Features.Tasks
{
    [HeavyEquipmentContext]
    public class GetTasksRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetTaskDto>>
    {
        /// <summary>
        /// Setting this value will return all tasks that belong to the specified contact.
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// Setting this value will return all tasks that belong to the specified user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Setting this value will return all tasks whose subject contains the search field.
        /// </summary>
        public string Search { get; set; }
    }

    public class GetTasksRequestHandler :
        IRequestHandler<GetTasksRequest, PagedQueryResult<GetTaskDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;
        private Dictionary<string, List<Tuple<int, string>>> _relatedRecordsDictionary;

        public GetTasksRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<GetTaskDto>>> HandleAsync(GetTasksRequest request)
        {
            SetRelatedRecordsDictionary();

            var result = new PagedQueryResult<GetTaskDto>();

            var tasks =
                (from t in _context.Set<Task>()
                    select new
                    {
                        t.TaskId,
                        t.UserId,
                        t.ContactId,
                        t.RelatedRecordId,
                        t.RelatedRecordType,
                        t.Subject,
                        t.Comments,
                        t.StartDate,
                        t.DueDate,
                        t.CompleteDate,
                        t.Status,
                        t.Priority,
                        t.ReminderDate,
                        t.ReminderRepeatSchedule
                    })
                .AsQueryable();

            if (request.ContactId > 0)
                tasks = tasks.Where(x => x.RelatedRecordId == request.ContactId && x.RelatedRecordType == RelatedRecord.Types.Contact);

            if (request.UserId > 0)
                tasks = tasks.Where(x => x.UserId == request.UserId);
            else
                tasks = tasks.Where(x => x.UserId == _identity.RequestingUser.UserId);

            if (!string.IsNullOrEmpty(request.Search))
                tasks = tasks.Where(x => x.Subject.Contains(request.Search) || x.Comments.Contains(request.Search));

            var count = tasks.Count();

            result.PageCount = count / request.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            result.Items = tasks.ProjectTo<GetTaskDto>()
                .GetPagedData(request)
                .ToList();

            SetRelatedRecordName(result);

            return result.AsResponseAsync();
        }

        private void SetRelatedRecordsDictionary()
        {
            _relatedRecordsDictionary = new Dictionary<string, List<Tuple<int, string>>>
            {
                ["Company"] = GetCompanyNameDictionary(),
                ["Contact"] = GetContactNameDictionary()
            };
        }

        private List<Tuple<int, string>> GetCompanyNameDictionary()
        {
            return _context.Set<Company>()
                .Where(x =>!x.Deleted)
                .Select(x => new Tuple<int, string>(x.CompanyId, x.CompanyName))
                .ToList();
        }

        private List<Tuple<int, string>> GetContactNameDictionary()
        {
            return _context.Set<Contact>()
                .Where(x => x.Active && !x.Deleted)
                .Select(x => new {x.ContactId, x.FirstName, x.LastName})
                .ToList()
                .Select(x => new Tuple<int, string>(x.ContactId, $"{x.FirstName} {x.LastName}"))
                .ToList();
        }

        private void SetRelatedRecordName(PagedQueryResult<GetTaskDto> result)
        {
            foreach (var item in result.Items)
            {
                item.RelatedRecordName = GetRelatedRecordName(item.RelatedRecordId,
                    item.RelatedRecordType);
            }
        }
        
        private string GetRelatedRecordName(int relatedRecordId, string relatedRecordType)
        {
            var validRelatedRecordTypes = new[]
            {
                RelatedRecord.Types.Company,
                RelatedRecord.Types.Contact,
            };

            if (!validRelatedRecordTypes.Contains(relatedRecordType))
                return null;

            return _relatedRecordsDictionary[relatedRecordType]
                .Where(x => x.Item1 == relatedRecordId)
                .Select(x => x.Item2)
                .SingleOrDefault();
        }
    }

    public class GetTasksValidator : AbstractValidator<GetTasksRequest>
    {
        public GetTasksValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.Search)
                .MaximumLength(25);
        }
    }
}
 