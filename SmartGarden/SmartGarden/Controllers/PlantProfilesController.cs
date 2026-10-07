using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGarden.Repositories;
using SmartGarden.Shared.DTOs;
using SmartGarden.Shared.Entities;

namespace SmartGarden.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantProfilesController : ControllerBase
    {
        private readonly IPlantProfileRepository _plantProfileRepository;

        public PlantProfilesController(IPlantProfileRepository plantProfileRepository)
        {
            _plantProfileRepository = plantProfileRepository;
        }

        [Authorize]
        [HttpGet("lookup")]
        public async Task<IActionResult> GetPlantProfilesLookup()
        {
            var profiles = await _plantProfileRepository.GetAllPlantProfilesAsync();

            var lookup = profiles.Select(p => new PlantProfileLookupDTO
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return Ok(lookup);
        }

        [Authorize]
        [HttpGet("species")]
        public async Task<IActionResult> GetSpecies()
        {
            var species = await _plantProfileRepository.GetAllSpeciesAsync();
            
            return species.Count > 0 ? Ok(species) : NotFound("No species found.");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProfile([FromBody] CreatePlantProfileDTO profile)
        {
            if (!Enum.TryParse<Species>(profile.Species, ignoreCase: true, out var speciesEnum))
            {
                return BadRequest(new { message = $"Species '{profile.Species}' is invalid." });
            }

            var newProfile = new PlantProfile
            {
                Name = profile.Name,
                Species = speciesEnum,
                Description = profile.Description,
                WateringFrequencyDays = profile.WateringFrequencyDays ?? 3,
                DaysToHarvest = profile.DaysToHarvest ?? 3
            };

            var createdProfile =  await _plantProfileRepository.AddPlantProfile(newProfile);
            
            return CreatedAtAction(nameof(GetProfileById), new { id = createdProfile.Id }, createdProfile);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var profile = await _plantProfileRepository.GetPlantProfileByIdAsync(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }
    }
}
