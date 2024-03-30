using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
public class AddUploadFileOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation?.OperationId?.ToLower() == "postrider")
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "file",
                In = ParameterLocation.Header,
                Description = "Upload File",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                }
            });
        }
    }

}
