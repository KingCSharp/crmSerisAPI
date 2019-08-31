using System.Net;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Api.Filters;
using crmSeries.Core.Features.Inspections;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/inspections")]
    [AcceptsApiKey]
    public class InspectionsController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all inspection types and available inspections
        /// </summary>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetInspectionTypeDto>))]
        public Task<IActionResult> GetInspectionTypes(GetAllInspectionTypesRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Gets a list of all available inspections for a given inspection type.
        /// </summary>
        [HttpGet("{typeId}")]
        [Produces(typeof(PagedQueryResult<GetInspectionDto>))]
        public Task<IActionResult> GetInspections([FromRoute] int typeId, PagedQueryRequest request)
        {
            return HandleAsync(new GetInspectionsByTypeRequest(typeId, request));
        }
        
        /// <summary>
        /// Gets an inspection's groups, items, images, and responses
        /// </summary>
        [HttpGet("{inspectionId}/template")]
        [Produces(typeof(GetInspectionTemplateDto))]
        public Task<IActionResult> GetInspectionTemplate([FromRoute] int inspectionId)
        {
            return HandleAsync(new GetInspectionTemplateRequest(inspectionId));
        }

        /// <summary>
        /// Gets an inspection's images
        /// </summary>
        [HttpGet("{inspectionId}/images")]
        [Produces(typeof(PagedQueryResult<GetInspectionImageDto>))]
        public Task<IActionResult> GetInspectionImages([FromRoute] int inspectionId, PagedQueryRequest request)
        {
            return HandleAsync(new GetInspectionImagesRequest(inspectionId, request));
        }

        /// <summary>
        /// Gets an inspection's groups
        /// </summary>
        [HttpGet("{inspectionId}/groups")]
        [Produces(typeof(PagedQueryResult<GetInspectionGroupDto>))]
        public Task<IActionResult> GetInspectionGroups([FromRoute] int inspectionId, PagedQueryRequest request)
        {
            return HandleAsync(new GetInspectionGroupsRequest(inspectionId, request));
        }

        /// <summary>
        /// Gets an inspection group's items
        /// </summary>
        [HttpGet("groups/{groupId}/items")]
        [Produces(typeof(PagedQueryResult<GetInspectionItemDto>))]
        public Task<IActionResult> GetInspectionItems([FromRoute] int groupId, PagedQueryRequest request)
        {
            return HandleAsync(new GetInspectionItemsRequest(groupId, request));
        }

        /// <summary>
        /// Gets an inspection item's responses
        /// </summary>
        [HttpGet("items/{itemId}/responses")]
        [Produces(typeof(PagedQueryResult<GetInspectionImageDto>))]
        public Task<IActionResult> GetInspectionResponses([FromRoute] int itemId, PagedQueryRequest request)
        {
            return HandleAsync(new GetInspectionResponsesRequest(itemId, request));
        }

        /// <summary>
        /// Upload and attach an image to an inspection item
        /// </summary>
        [HttpPost("{assignedInspectionId}/images/{assignedItemId}")]
        [Produces(typeof(Response<GetRecordAssignedInspectionItemImageDto>))]
        public Task<IActionResult> Upload([FromRoute] int assignedInspectionId, [FromRoute] int assignedItemId, IFormFile file)
        {
            var request = new AttachInspectionItemImageRequest
            {
                AssignedInspectionId = assignedInspectionId,
                AssignedItemId = assignedItemId,
                FileName = file.FileName,
                FileLength = (int)file.Length,
                FileStream = file.OpenReadStream()
            };

            return HandleAsync(request, HttpStatusCode.Created);
        }

        /// <summary>
        /// Adds a new inspection
        /// </summary>
        [HttpPost("record")]
        [Produces(typeof(Response<AddResponse>))]
        public Task<IActionResult> Post([FromBody] RecordAssignedInspectionDto inspection)
        {
            return HandleAsync(new SaveInspectionRequest(inspection));
        }

        /// <summary>
        /// Updates an existing inspection
        /// </summary>
        [HttpPut("record/{assignedInspectionId}")]
        [Produces(typeof(Response<AddResponse>))]
        public Task<IActionResult> Edit([FromRoute] int assignedInspectionId, [FromBody] RecordAssignedInspectionDto inspection)
        {
            return HandleAsync(new SaveInspectionRequest(assignedInspectionId, inspection));
        }
    }
}