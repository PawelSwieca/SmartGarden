using SmartGarden.Shared.Entities;
using SmartGarden.Data;
using Microsoft.EntityFrameworkCore;
namespace SmartGarden.Repositories
{
    public interface IPlantProfileRepository
    {
        public Task<PlantProfile?> GetPlantProfileByIdAsync(int id);
        public Task<ICollection<PlantProfile>> GetAllPlantProfilesAsync();
        public Task<ICollection<String>> GetPlantProfileNamesAsync();
        public Task<ICollection<PlantProfile>> GetProfilesBySpeciesAsync(Species species);
        public Task<ICollection<PlantProfile>> SearchProfilesByNameAsync(string searchTerm);
        public Task AddPlantProfile(PlantProfile plantProfile);
        public Task UpdatePlantProfile(PlantProfile plantProfile);
        public Task DeletePlantProfile(PlantProfile plantProfile);
    }

    public class  PlantProfileRepository : IPlantProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlantProfile?> GetPlantProfileByIdAsync(int id)
        {
            return await _context.PlantProfiles.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ICollection<PlantProfile>> GetAllPlantProfilesAsync()
        {
            return await _context.PlantProfiles.AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<String>> GetPlantProfileNamesAsync()
        {
            return await _context.PlantProfiles
                .Select(p => p.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<PlantProfile>> GetProfilesBySpeciesAsync(Species species)
        {
            return await _context.PlantProfiles
                .Where(p => p.Species == species)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<PlantProfile>> SearchProfilesByNameAsync(string searchTerm)
        {
            return await _context.PlantProfiles
                .Where(p => p.Name.Contains(searchTerm))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddPlantProfile(PlantProfile plantProfile)
        {
            _context.PlantProfiles.Add(plantProfile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlantProfile(PlantProfile plantProfile)
        {
            _context.PlantProfiles.Update(plantProfile);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlantProfile(PlantProfile plantProfile)
        {
            _context.PlantProfiles.Remove(plantProfile);
            await _context.SaveChangesAsync();
        }
    }
}
