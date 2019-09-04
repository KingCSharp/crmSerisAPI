using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace crmSeries.Api.Filters
{
    public class FileUploadFilter : IOperationFilter
    {
        private const string formDataMimeType = "multipart/form-data";

        private static readonly string[] formFilePropertyNames = typeof(IFormFile)
            .GetTypeInfo()
            .DeclaredProperties
            .Select(p => p.Name)
            .ToArray();

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null || operation.Parameters.Count == 0)
                return;

            var formFileParameters = context
                .ApiDescription
                .ActionDescriptor
                .Parameters
                .Where(x => x.ParameterType == typeof(IFormFile))
                .ToList();

            if (!formFileParameters.Any())
                return;

            var operationParams = operation.Parameters.ToArray();
            
            foreach (var parameter in operationParams)
            {
                if (formFilePropertyNames.Contains(parameter.Name))
                    operation.Parameters.Remove(parameter);
            }

            foreach (var formFileParameter in formFileParameters)
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = formFileParameter.Name,
                    In = "formData",
                    Description = "Upload File",
                    Type = "file",
                });
            }

            operation.Consumes.Add(formDataMimeType);
        }
    }
}
