using Microsoft.EntityFrameworkCore;
using TravelManagementSystem.Domain.Entities;
using TravelManagementSystem.Domain.Repositories.Interfaces;
using TravelManagementSystem.Infrastructure.Persistence;

namespace TravelManagementSystem.Infrastructure.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> FindByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username && x.IsActive);
        }

        public async Task AddUserAsync(User user)
        {
            user.CreatedOn = DateTime.UtcNow;
            user.IsActive = true;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
    }
}
