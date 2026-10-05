using Microsoft.EntityFrameworkCore;
using SmartGarden.Data;
using SmartGarden.Shared.Entities;

namespace SmartGarden.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByIdAsync(int id);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task AddUser(User user);
        public Task EditUser(User user);
        public Task DeleteUser(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task EditUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
