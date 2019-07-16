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
        public string ParentType { get; set; }

        public bool IsMachine { get; set; } = true;
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

            if (!string.IsNullOrEmpty(request.ParentType))
                equipments = equipments.Where(x => x.ParentType == request.ParentType);

            var count = equipments.Count();

            result.PageCount = count / request.PageSize;
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
            RuleFor(x => x.ParentType).Must(BeAValidParentType)
                .When(x => !string.IsNullOrEmpty(x.ParentType))
                .WithMessage(EquipmentConstants.ErrorMessages.InvalidParentType);

            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }

        private bool BeAValidParentType(string parentType)
        {
            return EquipmentConstants.ParentTypes.Contains(parentType);
        }
    }
}
