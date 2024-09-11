using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using StoryTeller.DTO;
using StoryTeller.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StoryTeller.SwaggerSchemaFilter;

public class UserSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(User) || context.Type == typeof(UserDTO))
        {
            schema.Example = new OpenApiObject
            {
                ["Id"] = new OpenApiInteger(1),
                ["Nome"] = new OpenApiString("Patrick Medeiros De Luca"),
                ["Nick"] = new OpenApiString("Mestre Henny"),
                ["Email"] = new OpenApiString("patrick_mdl@outlook.com"),
                ["Senha"] = new OpenApiString("senha123"),
            };
        }
    }
}