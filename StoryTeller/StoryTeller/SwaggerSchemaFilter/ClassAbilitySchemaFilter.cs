using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using StoryTeller.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StoryTeller.SwaggerSchemaFilter;

public class ClassAbilitySchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(ClassAbility) || context.Type == typeof(ClassAbilityDTO))
        {
            schema.Example = new OpenApiObject
            {
                ["Id"] = new OpenApiInteger(1),
                ["Nome"] = new OpenApiString("Ataque Especial"),
                ["Custo"] = new OpenApiString("2 PE"),
                ["Descricao"] = new OpenApiString(" Quando faz um ataque, você pode gastar 2 PE para receber +5 no teste de ataque ou na rolagem de dano. Conforme avança de NEX, você pode gastar +1 PE para receber mais bônus de +5 (veja a Tabela 1.3). Você pode aplicar cada bônus de +5 em ataque ou dano. Por exemplo, em NEX 55%, você pode gastar 4 PE para receber +5 no teste de ataque e +10 na rolagem de dano")
            };
        }
    }
}