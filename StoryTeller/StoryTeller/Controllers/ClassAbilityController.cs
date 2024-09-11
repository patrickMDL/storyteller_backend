
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoryTeller.AppDataContext;
using StoryTeller.Interface;
using StoryTeller.Models;
using StoryTeller.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace StoryTeller.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClassAbilityController : ControllerBase
    {
        private readonly IClassAbilityServices _classAbilityServices;

        public ClassAbilityController(IClassAbilityServices classAbilityServices)
        {
            _classAbilityServices = classAbilityServices;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Buscar todas as habilidades de classe", Description = "Retorna todas as habilidades de classe cadastradas no sistema.")]
        public async Task<IActionResult> GetAllAbilityClasses()
        {
            try
            {
                var abilitiesMap = await _classAbilityServices.GetAllAbilities();
                if (abilitiesMap == null || !abilitiesMap.Any())
                    return NotFound(new { message = "Nenhuma habilidade de classe encontrada." });
                return Ok(new { message = "Habilidades de classe encontradas.", data = abilitiesMap });
            }
            catch (Exception err)
            {
                return StatusCode(
                    500,
                    new { message = "Erro ao bustar todas as habilidades de classe.", err }
                    );
            }
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Buscar habilidade de classe por ID", Description = "Retorna uma habilidade de classe baseado no ID dela.")]
        public async Task<IActionResult> GetAbilityClassById(int Id)
        {
            try
            {
                var userMap = await _classAbilityServices.GetClassAbilityById(Id);
                if (userMap == null)
                    return NotFound("Habilidade de Classe não encontrada.");
                return Ok(new { message = "Habilidade de classe encontrada com sucesso.", data = userMap });
            }
            catch (Exception err)
            {
                return StatusCode(
                    500,
                    new { message = $"Erro ao buscar a classe de Id {Id}", err }
                    );
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Criar uma habilidades de classe", Description = "Insere uma nova habilidade de classe no banco")]
        public async Task<IActionResult> CreateAbilityClass(ClassAbility classAbility)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _classAbilityServices.CreateClassAbility(classAbility);
                return StatusCode(203, new {message = "Habilidade de classe criada com sucesso"});
            }
            catch (Exception err)
            {
                return StatusCode(
                    500,
                    new { message = "Erro ao criar habilidade de classe.", err });
            }
        }

        [HttpPut("{Id}")]
        [SwaggerOperation(Summary = "Atualizar uma habilidade de classe", Description = "Atualiza as informações de uma habilidade de classe.")]
        public async Task<IActionResult> UpdateAbilityClass(int Id, ClassAbilityDTO classAbility)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _classAbilityServices.UpdateClassAbility(Id, classAbility);
                return NoContent();
            }
            catch (Exception err)
            {
                return StatusCode(
                    500,
                    new { message = "Erro ao atualizar habilidade de classe.", err });
            }
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Remove as habilidades de classe", Description = "Remove a habilidade de classe pelo ID inserido.")]
        public async Task<IActionResult> DeleteAbilityClass(int Id)
        {
            try
            {
                await _classAbilityServices.DeleteClassAbility(Id);
                return NoContent();
            }
            catch (Exception err)
            {
                return StatusCode(
                    500,
                    new { message = "Erro ao remover habilidade de classe." });
            }
        }

    }
}