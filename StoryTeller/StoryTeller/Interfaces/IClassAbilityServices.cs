using StoryTeller.DTO;
using StoryTeller.Models;

namespace StoryTeller.Interface
{
    public interface IClassAbilityServices
    {
        Task<IEnumerable<ClassAbility>> GetAllAbilities();
        Task<ClassAbility> GetClassAbilityById(int Id);
        Task CreateClassAbility(ClassAbility user);
        Task UpdateClassAbility(int Id, ClassAbilityDTO user);
        Task DeleteClassAbility(int id);
    }
}