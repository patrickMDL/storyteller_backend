
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTeller.AppDataContext;
using StoryTeller.Interface;
using StoryTeller.Models;
using Exception = System.Exception;

namespace StoryTeller.Services
{
    public class ClassAbilityServices : IClassAbilityServices
    {
        private readonly StorytellerDbContext _context;
        private readonly ILogger<ClassAbilityServices> _logger;
        private readonly IMapper _mapper;

        public ClassAbilityServices(StorytellerDbContext context, ILogger<ClassAbilityServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassAbility>> GetAllAbilities()
        {
            try
            {
                var abilitiesMap = await _context.ClassAbility.ToListAsync();
                if (abilitiesMap == null)
                    throw new Exception("Nenhuma habilidade encontrada.");
                return abilitiesMap;
            }
            catch (Exception err)
            {
                throw new Exception("Erro ao buscar todas as habilidade.", err);
            }
        }

        public async Task<ClassAbility> GetClassAbilityById(int Id)
        {
            try
            {
                var abilityMap = await _context.ClassAbility.FindAsync(Id);
                if (abilityMap == null)
                    throw new Exception($"Erro ao encontrar habilidade de classe de Id-{Id}");
                return abilityMap;
            }
            catch (Exception err)
            {
                throw new Exception("Erro ao buscar habilidade de classe.", err);
            }
        }

        public async Task CreateClassAbility(ClassAbility classAbility)
        {
            try
            {
                _context.ClassAbility.Add(classAbility);
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception("Erro ao criar um usuário.", err);
            }
        }

        public async Task UpdateClassAbility(int Id, ClassAbilityDTO ability)
        {
            try
            {
                var abilityMap = await _context.ClassAbility.FindAsync(Id);
                if (abilityMap == null)
                    throw new Exception("Habilidade de Classe não encontrada.");

                if (ability.Nome != null)
                    abilityMap.Nome = ability.Nome;
                if (ability.Descricao != null)
                    abilityMap.Descricao = ability.Descricao;
                if (ability.Is_initial != null)
                    abilityMap.Is_initial = ability.Is_initial;
                if (ability.Custo != null)
                    abilityMap.Custo = ability.Custo;
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception("Erro ao atualizar o usuário.", err);
            }
        }

        public async Task DeleteClassAbility(int id)
        {
            try
            {
                var abilityClass = await _context.ClassAbility.FindAsync(id);
                if (abilityClass == null)
                    throw new Exception("Habilidade de classe não encontrada.");
                _context.ClassAbility.Remove(abilityClass);
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception("Erro ao remover a habilidade de classe.", err);
            }
        }
    }
}