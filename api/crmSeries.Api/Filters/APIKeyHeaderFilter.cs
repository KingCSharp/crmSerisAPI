using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace crmSeries.Api.Filters
{
    public class APIKeyHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "api-key",
                In = "header",
                Type = "string",
                Required = true,
                Description = "The API key used to authenticate requests. This is provided to customers by crmSeries."
            });

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "email",
                In = "header",
                Type = "string",
                Required = false,
                Description = "Email address of the current user. This is used only for requests that get records for the current user. This will be replaced by identity server."
            });
        }
    }
}
