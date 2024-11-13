using Microsoft.AspNetCore.Mvc;
using StoryTeller.Interface;
using Swashbuckle.AspNetCore.Annotations;

namespace StoryTeller.Controller;

[ApiController]
[Route("api/[Controller]")]
public class ClassController : ControllerBase
{
    private readonly IClassServices _classServices;

    public ClassController(IClassServices classServices)
    {
        _classServices = classServices;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Buscar todas as classes.")]
    public async Task<IActionResult> GetAllClasses()
    {
        try
        {
            var classMap = await _classServices.GetAllClasses();
            if (classMap == null || !classMap.Any())
                return NotFound(new { message = "Nenhuma classe encontrada." });
            return Ok(new { message = "Classes encontradas com sucesso.", data = classMap });
        }
        catch (Exception err)
        {
            return StatusCode(
                500,
                new { message = "Erro ao buscar todas as classes.", err }
                );
        }
    }
}