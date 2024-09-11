using Microsoft.AspNetCore.Mvc;
using StoryTeller.DTO;
using StoryTeller.Interface;
using StoryTeller.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace StoryTeller.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Busca todos os usuários.", Description = "Busca todos os usuários existentes no banco.")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var user = await _userServices.GetAllUsers();
                if (user == null || !user.Any())
                {
                    return NotFound(new { message = "Nenhum usuário encontrado." });
                }

                return Ok(new { message = "Usuários encontrados com suceso.", data = user });
            }
            catch (Exception err)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = "Ocorreu um erro ao buscar todos os usuários.",
                        error = err.Message
                    });
            }
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Busca usuário pelo Id.", Description = "Busca o usuário pelo Id único dele.")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            try
            {
                var userMap = await _userServices.GetUserById(Id);
                if (userMap == null)
                    return NotFound(new { message = "Usuário não encontrado." });
                return Ok(new { message = "Usuário encontrado.", data = userMap });
            }
            catch (Exception err)
            {
                return StatusCode(
                    500,
                    new { message = $"Ocorreu um erro ao buscar o usuário de Id {Id}." , err});
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um usuário novo.", Description = "Cria um usuário novo, a API ainda criptografa a senha antes de armazena-la no banco.")]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _userServices.CreateUser(user);
                return Ok(new { message = "Usuário criado com sucesso." });

            }
            catch (Exception err)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = "Ocorreu um erro aob criar o usuário.",
                        error = err.Message
                    });
            }
        }

        [HttpPut("{Id}")]
        [SwaggerOperation(Summary = "Atualiza a data do último login.", Description = "Endpoint serve para atualizar a data em que o usuário realizou seu último login.")]
        public async Task<IActionResult> UpdateLastLoginUser(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userMap = _userServices.GetUserById(Id);
                if (userMap == null)
                    return NotFound(new { message = $"Usuário de id {Id} não foi encontrado." });
                await _userServices.UpdateLastLoginUser(Id);
                return Ok(new { message = $"Usuário de Id {Id} atualizado com sucesso." });
            }
            catch (Exception err)
            {
                return StatusCode(
                    500,
                    new { message = $"Não foi possível atualizar o último login do usuário." }
                    );
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove o usuário.", Description = "Apaga um usuário do banco de dados.")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            try
            {
                await _userServices.DeleteUser(id);
                return Ok(new { message = $"User com Id {id} removido com sucesso." });
            }
            catch (Exception err)
            {
                return StatusCode(
                    500,
                    new { message = $"Ocorreu um erro ao remover o usuário de id {id}."});
            }
        }
    }
}

