using SmartGarden.Shared.Entities;
using SmartGarden.Data;
using Microsoft.EntityFrameworkCore;
namespace SmartGarden.Repositories
{
    public interface IPlantedCropRepository
    {
        public Task<PlantedCrop?> GetPlantedCropByIdAsync(int id);
        public Task<ICollection<PlantedCrop>> GetCropsByGardenIdAsync(int gardenId);
        public Task<ICollection<PlantedCrop>> GetCropsNeedingWateringAsync();
        public Task<ICollection<PlantedCrop>> GetCropsReadyForHarvestAsync();
        public Task AddPlantedCrop(PlantedCrop plantedCrop);
        public Task UpdatePlantedCrop(PlantedCrop plantedCrop);
        public Task DeletePlantedCrop(PlantedCrop plantedCrop);
    }

    public class PlantedCropRepository : IPlantedCropRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantedCropRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlantedCrop?> GetPlantedCropByIdAsync(int id)
        {
            
            return await _context.PlantedCrops
                .Include(c => c.Profile) 
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<PlantedCrop>> GetCropsByGardenIdAsync(int gardenId)
        {
            return await _context.PlantedCrops
                .Include(c => c.Profile) 
                .Where(c => c.GardenId == gardenId)
                .AsNoTracking() 
                .ToListAsync();
        }

        public async Task<ICollection<PlantedCrop>> GetCropsNeedingWateringAsync()
        {
            var currentDate = DateTime.UtcNow;

            return await _context.PlantedCrops
                .Include(c => c.Profile)
                .Where(c => !c.IsHarvested &&
                            (c.LastWateredDate == null ||
                             currentDate >= c.LastWateredDate.Value.AddDays(c.Profile.WateringFrequencyDays)))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<PlantedCrop>> GetCropsReadyForHarvestAsync()
        {
            var currentDate = DateTime.UtcNow;
            return await _context.PlantedCrops
                .Include(c => c.Profile)
                .Where(c => !c.IsHarvested && c.EstimatedHarvestDate <= currentDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddPlantedCrop(PlantedCrop plantedCrop)
        {
            _context.PlantedCrops.Add(plantedCrop);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlantedCrop(PlantedCrop plantedCrop)
        {
            _context.PlantedCrops.Update(plantedCrop);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlantedCrop(PlantedCrop plantedCrop)
        {
            _context.PlantedCrops.Remove(plantedCrop);
            await _context.SaveChangesAsync();
        }
    }
}
