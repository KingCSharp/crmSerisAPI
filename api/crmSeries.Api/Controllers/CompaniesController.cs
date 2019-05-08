using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/companies")]
    public class CompaniesController : BaseApiController
    {
        /// <summary>
        /// Gets a list of companies assigned to a user.
        /// </summary>
        /// <param name="email">The email of the current user.</param>
        // GET: api/companies/getcompanies
        [HttpGet]
        [Produces(typeof(IEnumerable<Company>))]
        [Route("{email}", Name ="GetCompanies")]
        public Task<IActionResult> GetCompanies(string email)
        {
            return HandleAsync(new GetCompaniesRequest
            {
                UserEmail = email
            });
        }

        /// <summary>
        /// Gets a single company by id.
        /// </summary>
        /// <param name="id">The id of the company.</param>
        // GET: api/companies/getcompany
        [HttpGet]
        [Produces(typeof(Company))]
        [Route("{id:int}", Name = "GetCompany")]
        public Task<IActionResult> GetCompany(int id)
        {
            return HandleAsync(new GetCompanyRequest
            {
                CompanyId = id
            });
        }
    }
}