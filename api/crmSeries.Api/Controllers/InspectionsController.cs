using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Features.Inspections;
using crmSeries.Core.Logic.Queries;
using Microsoft.AspNetCore.Mvc;
using crmSeries.Core.Mediator;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/inspectiontypes")]
    public class InspectionsController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all inspections 
        /// </summary>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetInspectionTypeDto>))]
        public Task<IActionResult> GetInspectionType(GetInspectionTypeRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Gets a list of all Inspection types.
        /// </summary>
        [HttpGet("inspections/{typeId}")]
        [Produces(typeof(PagedQueryResult<GetInspectionDto>))]
        public Task<IActionResult> GetInspection(int typeId)
        {
            return HandleAsync(new GetInspectionsRequest(typeId));
        }

        /// <summary>
        /// Adds a new inspection
        /// </summary>
        [HttpPost("/inspection/create")]
        [Produces(typeof(Response<AddResponse>))]
        public Task<IActionResult> Post([FromBody]AddInspectionRequest request)
        {
            return HandleAsync(request);
        }

        [HttpGet("/inspection/{inspectionId}/group")]
        [Produces(typeof(PagedQueryResult<GetInspectionGroupsDto>))]
        public Task<IActionResult> GetInspectionGroups(int inspectionId)
        {
            return HandleAsync(new GetInspectionGroupsRequest(inspectionId));
        }

        [HttpGet("/inspection/group/{groupid}/Item")]
        [Produces(typeof(PagedQueryResult<GetInspectionItemsDto>))]
        public Task<IActionResult> GetInspectionItems(int groupid)
        {
            return HandleAsync(new GetInspectionItemsRequest(groupid));
        }

        [HttpGet("/inspection/image/{inspectionId}")]
        [Produces(typeof(PagedQueryResult<GetInspectionImagesDto>))]
        public Task<IActionResult> GetInspectionImages(int inspectionId)
        {
            return HandleAsync(new GetInspectionImagesRequest(inspectionId));
        }

        [HttpGet("/inspection/item/response/{itemId}")]
        [Produces(typeof(PagedQueryResult<GetInspectionImagesDto>))]
        public Task<IActionResult> GetInspectionResponses(int itemId)
        {
            return HandleAsync(new GetInspectionResponsesRequest(itemId));
        }

    }
}