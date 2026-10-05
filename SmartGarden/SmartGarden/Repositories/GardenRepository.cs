using Microsoft.EntityFrameworkCore;
using SmartGarden.Shared.Entities;
using SmartGarden.Data;


namespace SmartGarden.Repositories
{
    public interface IGardenRepository
    {
        public Task<Garden?> GetGardenByUserNameAsync(string userName);
        public Task<ICollection<Garden>> GetGardensByUserNameAsync(string userName);
        public Task<Garden?> GetGardenByIdAsync(int id);
        public Task AddGarden(Garden garden);
        public Task EditGarden(Garden garden);
        public Task DeleteGarden(Garden garden);

    }

    public class GardenRepository : IGardenRepository
    {
        private readonly ApplicationDbContext _context;
        public GardenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Garden?> GetGardenByUserNameAsync(string userName)
        {
            DbSet<Garden> gardens = _context.Gardens;
            return await gardens
                .Where(g => g.User.UserName == userName)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task<ICollection<Garden>> GetGardensByUserNameAsync(string userName)
        {
            DbSet<Garden> gardens = _context.Gardens;
            return await gardens
                .Where(g => g.User.UserName == userName)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Garden?> GetGardenByIdAsync(int id)
        {
            return await _context.Gardens.FindAsync(id);
        }
        public async Task AddGarden(Garden garden)
        {
            _context.Gardens.Add(garden);
            await _context.SaveChangesAsync();
        }
        public async Task EditGarden(Garden garden)
        {
            try
            {
                
                _context.Gardens.Update(garden);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                
                Console.WriteLine($"Error updating garden: {ex.Message}");
                throw; 
            }
        }
        public async Task DeleteGarden(Garden garden)
        {
            try
            {
                var existingGarden = await _context.Gardens.FindAsync(garden.Id);
                if (existingGarden == null)
                {
                    throw new Exception($"Garden with ID {garden.Id} not found.");
                }

                
                _context.Gardens.Remove(existingGarden);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting garden: {ex.Message}");
                
                throw;
            }
        }
    }
}
