using System.Collections;
using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using System.Linq;
using FluentValidation;
using crmSeries.Core.Mediator;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Domain.HeavyEquipment;
using System.Collections.Generic;
using crmSeries.Core.Features.Equipment.Utility;

namespace crmSeries.Core.Features.Equipment
{
    [HeavyEquipmentContext]
    public class GetEquipmentRequest : IRequest<PagedQueryResult<GetEquipmentDto>>
    {
        /// <summary>
        /// The paging information of the paged object.
        /// </summary>
        public PagedQueryRequest PageInfo { get; set; }

        /// <summary>
        /// The identifier of the branch associated with the equipment.
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// The equipment type.  Valid types are either New or Used
        /// </summary>
        public string EquipmentType { get; set; }

        /// <summary>
        /// The statuses of the equipment.
        /// </summary>
        public List<string> Statuses { get; set; }
    }

    public class GetEquipmentHandler :
        IRequestHandler<GetEquipmentRequest, PagedQueryResult<GetEquipmentDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetEquipmentHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetEquipmentDto>>> HandleAsync(GetEquipmentRequest request)
        {
            var result = new PagedQueryResult<GetEquipmentDto>();

            var equipment =
                (from e in _context.Set<Domain.HeavyEquipment.Equipment>()
                 join cat in _context.Set<EquipmentCategory>().DefaultIfEmpty()
                     on e.CategoryId equals cat.CategoryId
                 join branch in _context.Set<Branch>()
                     on e.CurrentBranchId equals branch.BranchId
                 where !e.Deleted && e.Inventory && e.AvailableForQuote == true &&
                       (request.BranchId > 0 ? branch.BranchId == request.BranchId : true) &&
                       (!string.IsNullOrEmpty(request.EquipmentType) ? e.NewUsed == request.EquipmentType : true) &&
                       (request.Statuses.Any() ? request.Statuses.Contains(e.Status) : true)
                 select new
                 {
                     e.EquipmentId,
                     e.ParentId,
                     e.ParentType,
                     e.Machine,
                     e.Make,
                     e.Model,
                     e.StockNo,
                     e.SerialNo,
                     e.Description,
                     e.EquipYear,
                     e.Fleet,
                     e.FleetType,
                     e.Status,
                     e.NewUsed,
                     e.ListPrice,
                     e.SellPrice,
                     e.AcquisitionDate,
                     e.AvailableForQuote,
                     e.InventoryNotes,
                     e.Class,
                     e.InOut,
                     e.LastSmr,
                     e.InventorySpecs,
                     cat.Category,
                     branch.BranchName,
                 })
                .AsQueryable();


            var count = equipment.Count();

            result.Items = equipment
                .ProjectTo<GetEquipmentDto>()
                .ToList();

            result.PageCount = count / request.PageInfo.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageInfo.PageNumber;
            result.PageSize = request.PageInfo.PageSize;

            return result.AsResponseAsync();
        }
    }

    public class GetEquipmentValidator : AbstractValidator<GetEquipmentRequest>
    {
        public GetEquipmentValidator()
        {
            RuleFor(x => x.PageInfo.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageInfo.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.Statuses).Must(AllBeLessThanMaxLimitAllowed)
                .WithMessage(EquipmentConstants.ErrorMessages.ExceededStatusMaxLength);
        }

        private bool AllBeLessThanMaxLimitAllowed(List<string> statuses)
        {
            return statuses.All(x => x.Length <= EquipmentConstants.StatusMaxLength);
        }
    }
}
