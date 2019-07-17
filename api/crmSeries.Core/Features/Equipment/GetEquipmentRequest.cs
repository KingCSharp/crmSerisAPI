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
using crmSeries.Core.Features.Equipment.Dtos;
using crmSeries.Core.Features.Equipment;
using System;

namespace crmSeries.Core.Features.Notes
{
    [HeavyEquipmentContext]
    public class GetEquipmentRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetEquipmentDto>>
    {
        /// <summary>
        /// The type of the parent of this equipment.
        /// </summary>
        public string ParentType { get; set; }

        /// <summary>
        /// Represents if the equipment is a machine type or not.  Default
        /// is true.
        /// </summary>
        public bool IsMachine { get; set; } = true;

        /// <summary>
        /// The identifier of the category the equipment is classified as.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// The minimum number of hours the equipment has since it began
        /// use.
        /// </summary>
        public decimal StartHoursMin { get; set; } = decimal.MinValue;

        /// <summary>
        /// The maximum number of hours the equipment has since it began
        /// use.
        /// </summary>
        public decimal StartHoursMax { get; set; } = decimal.MaxValue;
    }

    public class GetEquipmentRequestHandler :
        IRequestHandler<GetEquipmentRequest, PagedQueryResult<GetEquipmentDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;

        public GetEquipmentRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<GetEquipmentDto>>> HandleAsync(GetEquipmentRequest request)
        {
            var result = new PagedQueryResult<GetEquipmentDto>();

            var equipments =
                (from e in _context.Set<Domain.HeavyEquipment.Equipment>()
                 where e.Active == true && !e.Deleted && e.Machine == request.IsMachine
                 select new
                 {
                     e.EquipmentId,
                     e.CategoryId,
                     e.ParentId,
                     e.ParentType,
                     e.Make,
                     e.Model,
                     e.Description,
                     e.SerialNo,
                     e.StockNo,
                     e.EquipmentNo,
                     e.EquipYear,
                     e.StartDate,
                     e.StartHours,
                     e.LastSmrdate,
                     e.LastSmr,
                     e.Latitude,
                     e.Longitude,
                     e.HoursPerDay,
                     e.Inventory,
                     e.Fleet,
                     e.FleetType,
                     e.Status,
                     e.NewUsed,
                     e.GroupCode
                 })
                .AsQueryable();

            if (request.CategoryId != default)
                equipments = equipments.Where(x => x.CategoryId == request.CategoryId);

            if (!string.IsNullOrEmpty(request.ParentType))
                equipments = equipments.Where(x => x.ParentType == request.ParentType);

            if (request.StartHoursMin != default || request.StartHoursMax != default)
                equipments = equipments.Where(x => x.StartHours >= request.StartHoursMin && x.StartHours <= request.StartHoursMax);

            var count = equipments.Count();

            result.PageCount = (count + request.PageSize - 1) / request.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            result.Items = equipments.ProjectTo<GetEquipmentDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }

    public class GetEquipmentValidator : AbstractValidator<GetEquipmentRequest>
    {
        public GetEquipmentValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.CategoryId)
                .GreaterThan(-1);

            RuleFor(x => x.StartHoursMin)
                .LessThanOrEqualTo(x => x.StartHoursMax)
                .When(x => x.StartHoursMin != default);

            RuleFor(x => x.ParentType).Must(BeAValidParentType)
                .When(x => !string.IsNullOrEmpty(x.ParentType))
                .WithMessage(EquipmentConstants.ErrorMessages.InvalidParentType);
        }

        private bool BeAValidParentType(string parentType)
        {
            return EquipmentConstants.ParentTypes.Contains(parentType);
        }
    }
}
