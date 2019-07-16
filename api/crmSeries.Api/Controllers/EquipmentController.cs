using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Equipment;
using crmSeries.Core.Features.Equipment.Dtos;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Logic.Queries;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/equipment")]
    public class EquipmentController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all equipment 
        /// </summary>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetEquipmentDto>))]
        public Task<IActionResult> GetEquipment(GetEquipmentRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Gets a list of all equipment categories.
        /// </summary>
        [HttpGet("categories")]
        [Produces(typeof(PagedQueryResult<EquipmentCategoryDto>))]
        public Task<IActionResult> GetEquipmentCategories(GetEquipmentCategoriesRequest request)
        {
            return HandleAsync(request);
        }
    }
}