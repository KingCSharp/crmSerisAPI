using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace crmSeries.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AcceptsApiKeyAttribute : Attribute
    {
        public bool IsRequired { get; }

        public AcceptsApiKeyAttribute(bool isRequired = false)
        {
            IsRequired = isRequired;
        }
    }

    public class APIKeyHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "email",
                In = "header",
                Type = "string",
                Required = false,
                Description = "Email address of the current user. This is used only for requests that get records for the current user. This will be replaced by identity server."
            });

            var attr = context.MethodInfo?.GetCustomAttributes(typeof(AcceptsApiKeyAttribute), true);
            if (attr == null || attr.Length == 0)
                attr = context.MethodInfo?.DeclaringType?.GetCustomAttributes(typeof(AcceptsApiKeyAttribute), true);

            if (attr != null && attr.Length > 0)
            {
                var isRequired = ((AcceptsApiKeyAttribute)attr[0]).IsRequired;

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "api-key",
                    In = "header",
                    Type = "string",
                    Required = isRequired,
                    Description = $"The API key used to authenticate requests. This is provided to customers by crmSeries.{(!isRequired ? "This is required if a valid Bearer token is not included with the request." : "")}"
                });

                if (isRequired)
                    operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
            }
        }
    }
}
