using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Inspections;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/inspections")]
    public class InspectionsController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all Inspection Types and Available Inspections
        /// </summary>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetInspectionTypeDto>))]
        public Task<IActionResult> GetInspectionType(GetAllInspectionTypesRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Gets an Inspection's groups, items, images, and responses
        /// </summary>
        [HttpGet("{inspectionId}/template")]
        [Produces(typeof(GetInspectionTemplateDto))]
        public Task<IActionResult> GetInspectionTemplate(int inspectionId)
        {
            return HandleAsync(new GetInspectionTemplateRequest(inspectionId));
        }

        /// <summary>
        /// Gets a list of all Inspections for a given Inspection Type.
        /// </summary>
        [HttpGet("{typeId}")]
        [Produces(typeof(PagedQueryResult<GetInspectionDto>))]
        public Task<IActionResult> GetInspection(int typeId)
        {
            return HandleAsync(new GetInspectionsByTypeRequest(typeId));
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
        [Produces(typeof(PagedQueryResult<GetInspectionGroupDto>))]
        public Task<IActionResult> GetInspectionGroups(int inspectionId)
        {
            return HandleAsync(new GetInspectionGroupsRequest(inspectionId));
        }

        [HttpGet("/inspection/group/{groupid}/Item")]
        [Produces(typeof(PagedQueryResult<GetInspectionItemDto>))]
        public Task<IActionResult> GetInspectionItems(int groupid)
        {
            return HandleAsync(new GetInspectionItemsRequest(groupid));
        }

        [HttpGet("/inspection/image/{inspectionId}")]
        [Produces(typeof(PagedQueryResult<GetInspectionImageDto>))]
        public Task<IActionResult> GetInspectionImages(int inspectionId)
        {
            return HandleAsync(new GetInspectionImagesRequest(inspectionId));
        }

        [HttpGet("/inspection/item/response/{itemId}")]
        [Produces(typeof(PagedQueryResult<GetInspectionImageDto>))]
        public Task<IActionResult> GetInspectionResponses(int itemId)
        {
            return HandleAsync(new GetInspectionResponsesRequest(itemId));
        }

    }
}