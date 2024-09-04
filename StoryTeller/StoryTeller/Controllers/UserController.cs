using Microsoft.AspNetCore.Mvc;
using StoryTeller.DTO;
using StoryTeller.Interface;
using StoryTeller.Models;

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
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var user = await _userServices.GetAllUsers();
                if (user == null || !user.Any())
                {
                    return Ok(new { message = "Nenhum usuário encontrado." });
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

        [HttpPost]
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

        [HttpPut]
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

        [HttpDelete]
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

