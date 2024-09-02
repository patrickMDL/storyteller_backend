using Microsoft.AspNetCore.Mvc;
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

                return Ok(new { message = "Usuários encontrados com suceso." });
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
    }
}

