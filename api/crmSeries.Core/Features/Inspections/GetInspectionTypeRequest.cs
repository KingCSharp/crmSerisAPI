using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using FluentValidation;
using crmSeries.Core.Mediator;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Extensions;
using System.Threading.Tasks;
using crmSeries.Core.Features.Inspections.Dtos;

namespace crmSeries.Core.Features.Inspections
{
    [HeavyEquipmentContext]
    public class GetInspectionTypeRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetInspectionTypeDto>>
    {
        /// <summary>
        /// The identifier of the InspectionType is classified as.
        /// </summary>
        public int InspectionTypeId { get; set; }
        /// <summary>
        /// The name of InspectionType. 
        /// </summary>
        public string InspectionTypeName { get; set; }


    }

    public class GetInspectionTypeRequestHandler :
        IRequestHandler<GetInspectionTypeRequest, PagedQueryResult<GetInspectionTypeDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;

        public GetInspectionTypeRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<GetInspectionTypeDto>>> HandleAsync(GetInspectionTypeRequest request)
        {
            var result = new PagedQueryResult<GetInspectionTypeDto>();

            var InspectionTypes =
                (from it in _context.Set<Domain.HeavyEquipment.InspectionType>()
                     //join i in _context.Set<Domain.HeavyEquipment.Inspection>() on it.TypeId equals i.TypeId
                     //into iit from iit2 in iit.DefaultIfEmpty()
                 select new
                 {
                     InspectionTypeName = it.Type,
                     InspectionTypeId = it.TypeId,
                     Inspections = (from i in _context.Set<Domain.HeavyEquipment.Inspection>() where i.TypeId == it.TypeId select new { InspectionId = i.TypeId, InspectionName = i.Name })

                 })
                .AsQueryable();


            result.Items = InspectionTypes.ProjectTo<GetInspectionTypeDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }

    public class GetInspectionValidator : AbstractValidator<GetInspectionTypeRequest>
    {
        public GetInspectionValidator()
        {
            //    RuleFor(x => x.PageNumber)
            //        .GreaterThan(0);

            //    RuleFor(x => x.PageSize)
            //        .GreaterThan(0);

            //    RuleFor(x => x.CategoryId)
            //        .GreaterThan(-1);

            //    RuleFor(x => x.StartHoursMin)
            //        .LessThanOrEqualTo(x => x.StartHoursMax)
            //        .When(x => x.StartHoursMin != default);

            //    RuleFor(x => x.ParentType).Must(BeAValidParentType)
            //        .When(x => !string.IsNullOrEmpty(x.ParentType))
            //        .WithMessage(InspectionConstants.ErrorMessages.InvalidParentType);
        }

        //private bool BeAValidParentType(string parentType)
        //{
        //   // return InspectionConstants.ParentTypes.Contains(parentType);
        //}
    }
}
