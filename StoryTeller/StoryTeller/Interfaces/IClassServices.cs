using StoryTeller.DTO;
using StoryTeller.Models;

namespace StoryTeller.Interface;

public interface IClassServices
{
    Task<IEnumerable<Class>> GetAllClasses();
    Task<Class> GetClassById(int id);
    Task CreateClass(Class classe); //Classe em pt pois class é uma palavra reservada.
    Task UpdateClass(int Id, ClassDTO classe);
    Task DeleteUser(int id);
}