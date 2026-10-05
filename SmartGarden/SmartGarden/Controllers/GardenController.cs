using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGarden.Repositories;
using SmartGarden.Shared.DTOs;
using SmartGarden.Shared.Entities;

namespace SmartGarden.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GardenController : ControllerBase
    {
        private readonly IGardenRepository _gardenRepository;
        private readonly IUserRepository _userRepository;

        public GardenController(IGardenRepository gardenRepository, IUserRepository userRepository)
        {
            _gardenRepository = gardenRepository;
            _userRepository = userRepository;
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetAllGardens() 
        {
            var userName = User.Identity!.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated properly.");
            }

            return Ok(await _gardenRepository.GetGardensByUserNameAsync(userName));
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddGarden([FromBody] CreateGardenDTO garden)
        {
            var userName = User.Identity!.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User is not authenticated properly.");
            }
            var user = await _userRepository.GetUserByUsernameAsync(userName);
            if (user == null)
            {
                return NotFound("User not found in the database.");
            }
            
            var newGarden = new Garden
            {
                Name = garden.Name,
                Latitude = garden.Latitude,
                Longitude = garden.Longitude,
                User = user, 

                
            };

            
            await _gardenRepository.AddGarden(newGarden);

            return Ok(new { Message = "Garden created successfully!" });
        }

        [Authorize]
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetGarden(int Id)
        {
            var garden = await _gardenRepository.GetGardenByIdAsync(Id);
            if (garden == null)
            {
                return NotFound("Garden not found.");
            }

            return Ok(garden);
        }

        [Authorize]
        [HttpPut("{Id:int}/name")]
        public async Task<IActionResult> UpdateGardenName(int Id, [FromBody] UpdateGardenNameDTO garden)
        {
            var existingGarden = await _gardenRepository.GetGardenByIdAsync(Id);
            if (existingGarden == null)
            {
                return NotFound("Garden not found.");
            }

            existingGarden.Name = garden.Name;

            await _gardenRepository.EditGarden(existingGarden);

            return Ok(new { Message = "Garden updated successfully!" });
        }
    }
}
