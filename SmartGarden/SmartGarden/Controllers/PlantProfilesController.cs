using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGarden.Repositories;
using SmartGarden.Shared.DTOs;

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
    }
}
