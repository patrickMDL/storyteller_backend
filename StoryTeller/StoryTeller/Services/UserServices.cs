using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StoryTeller.AppDataContext;
using StoryTeller.Controller;
using StoryTeller.DTO;
using StoryTeller.Interface;
using StoryTeller.Models;

namespace StoryTeller.Services
{
    public class UserServices : IUserServices
    {
        private readonly StorytellerDbContext _context;
        private readonly ILogger<UserServices> _logger;
        private readonly IMapper _mapper;

        public UserServices(StorytellerDbContext context, ILogger<UserServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var userMap = await _context.User.ToListAsync();
            if (userMap == null)
            {
                throw new Exception("Nenhum usuario encontrado.");
            }

            return userMap;
        }

        public async Task<User> GetUserById(int Id)
        {
            try
            {
                var userMap = await _context.User.FindAsync(Id);
                if (userMap == null)
                    throw new Exception("Usuário não encontrado.");
                return userMap;
            }
            catch (Exception err)
            {
                throw new Exception("Erro ao encontrar o usuário.");
            }
        }

        public async Task CreateUser(User user)
        {
            try
            {
                var senha = RandomNumberGenerator.GetBytes(128 / 8);
                var senhaCriptografada = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: user.Senha!,
                    salt: senha,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
                user.Senha = senhaCriptografada;

                var userMap = _context.User.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception("Ocorreu um erro ao criar o usuário.");
            }
        }

        public async Task UpdateUser(int Id, UserDTO user)
        {
            throw new NotImplementedException();
            // Função de atualizar usuário desnecessária. Será necessário implementar
            // uma função específica para email e outra para senha.
        }

        public async Task DeleteUser(int id)
        {
            try
            {
                var usermap = await _context.User.FindAsync(id);
                if (usermap != null)
                {
                    _context.User.Remove(usermap);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception(($"User Id {id} não encontado."));
                }
            }
            catch (Exception err)
            {
                throw new(message: "Ocorreu um erro ao deletar o usuário");
            }
        }
    }
}

