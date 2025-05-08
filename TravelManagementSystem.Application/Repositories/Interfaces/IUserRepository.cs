using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> FindByUsernameAsync(string username);

        Task AddUserAsync(User user);

        Task<bool> UsernameExistsAsync(string username);

        Task<bool> EmailExistsAsync(string email);
    }
}
