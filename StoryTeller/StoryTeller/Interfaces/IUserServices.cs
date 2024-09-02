using StoryTeller.DTO;
using StoryTeller.Models;

namespace StoryTeller.Interface
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int Id);
        Task CreateUser(User user);
        Task UpdateUser(int Id, UserDTO user);
        Task DeleteUser(int id);
    }
}