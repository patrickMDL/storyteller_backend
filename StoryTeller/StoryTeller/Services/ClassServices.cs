using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTeller.AppDataContext;
using StoryTeller.DTO;
using StoryTeller.Interface;
using StoryTeller.Models;

namespace StoryTeller.Services;

public class ClassServices : IClassServices
{
    private readonly StorytellerDbContext _context;
    private readonly ILogger<ClassServices> _logger;
    private readonly IMapper _mapper;

    public ClassServices(StorytellerDbContext context, ILogger<ClassServices> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Class>> GetAllClasses()
    {
        var classMap = await _context.Class.ToListAsync();
        if (classMap == null)
        {
            throw new Exception("Nenhuma classe encontrada.");
        }

        return classMap;
    }

    public async Task<Class> GetClassById(int id)
    {
        try
        {
            var classMap = await _context.Class.FindAsync(id);
            if (classMap == null)
                throw new Exception("Classe não encontrada.");
            return classMap;
        }
        catch (Exception err)
        {
            throw new Exception("Erro ao encontrar a class.", err);
        }
    }

    public async Task CreateClass(Class classe)
    {
        try
        {
            var userMap = _context.Class.Add(classe);
            await _context.SaveChangesAsync();
        }
        catch (Exception err)
        {
            throw new Exception("Ocorreu um erro ao criar a classe.", err);
        }
    }

    public async Task UpdateClass(int Id, ClassDTO classe)
    {
        try
        {
            var classMap = await _context.Class.FindAsync(Id);
            if (classMap == null)
                throw new Exception("Classe não encontrada.");

            if (classe.Pe_inicial != null)
                classMap.Pe_inicial = classe.Pe_inicial;
            if (classe.Pe_level != null)
                classMap.Pe_level = classe.Pe_level;
            if (classe.Pv_inicial != null)
                classMap.Pv_inicial = classe.Pv_inicial;
            if (classe.Pv_level != null)
                classMap.Pv_level = classe.Pv_level;
            if (classe.San_inicial != null)
                classMap.San_inicial = classe.San_inicial;
            if (classe.San_level != null)
                classMap.San_level = classe.San_level;
            await _context.SaveChangesAsync();
        }
        catch (Exception err)
        {
            throw new Exception("Erro ao atualizar a classe.", err);
        }
    }

    public async Task DeleteUser(int id)
    {
        try
        {
            var classMap = await _context.Class.FindAsync(id);
            if (classMap == null)
                throw new Exception("Habilidade de classe não encontrada.");
            _context.Class.Remove(classMap);
            await _context.SaveChangesAsync();
        }
        catch (Exception err)
        {
            throw new Exception("Erro ao remover a classe.", err);
        }
    }
}