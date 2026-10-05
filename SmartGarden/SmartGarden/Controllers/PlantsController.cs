using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGarden.Repositories;
using SmartGarden.Shared.DTOs;
using SmartGarden.Shared.Entities;
namespace SmartGarden.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantsController : ControllerBase
    {
        private readonly IPlantedCropRepository _plantCropRepository;
        public PlantsController(IPlantedCropRepository plantCropRepository)
        {
            _plantCropRepository = plantCropRepository;
        }

        [Authorize]
        [HttpGet("garden/{gardenId:int}")]
        public async Task<IActionResult> GetAllPlants(int gardenId)
        {
            var plants = await _plantCropRepository.GetCropsByGardenIdAsync(gardenId);

            var result = plants.Select(p => new PlantCropDTO
            {
                Id = p.Id,
                PlantName = p.Profile.Name,
                Species = p.Profile.Species.ToString(),
                Area = p.Area,
                PlantingDate = p.PlantingDate,
                EstimatedHarvestDate = p.EstimatedHarvestDate.Value,
                IsHarvested = p.IsHarvested,
                HarvestAmount = p.YieldWeight ?? 0,
                LastWateredDate = p.LastWateredDate ?? DateTime.MinValue,
            });

            return Ok(result);
        }
        [Authorize]
        [HttpPut("{plantId:int}/inspect")]
        public async Task<IActionResult> InspectPlant(int plantId, [FromBody] InspectPlantDTO inspectPlantDTO)
        {
            var plant = await _plantCropRepository.GetPlantedCropByIdAsync(plantId);
            if (plant == null)
            {
                return NotFound();
            }
            plant.Harvest(inspectPlantDTO.IsHarvested, inspectPlantDTO.HarvestAmount);
            await _plantCropRepository.UpdatePlantedCrop(plant);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{plantId:int}/water")]
        public async Task<IActionResult> WaterPlant(int plantId)
        {
            var plant = await _plantCropRepository.GetPlantedCropByIdAsync(plantId);
            if (plant == null) return NotFound();

            plant.Water();
            await _plantCropRepository.UpdatePlantedCrop(plant);

            return NoContent();
        }

        [Authorize]
        [HttpPost("new")]
        public async Task<IActionResult> PlantNewCrop([FromBody] CreatePlantDTO plantNewCropDTO)
        {
            var daysToHarvest = (int)(plantNewCropDTO.EstimatedHarvestDate - plantNewCropDTO.PlantingDate).TotalDays;
            var newCrop = new PlantedCrop(plantNewCropDTO.Area, plantNewCropDTO.PlantingDate, plantNewCropDTO.GardenId, plantNewCropDTO.PlantProfileId
                , daysToHarvest);

            await _plantCropRepository.AddPlantedCrop(newCrop);
            return Ok(new { Message = "Plant added successfully." });


        }
    }
}
