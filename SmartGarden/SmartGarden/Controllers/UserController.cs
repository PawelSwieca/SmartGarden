using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartGarden.Repositories;
using SmartGarden.Shared.DTOs;
using SmartGarden.Shared.Entities;
using System.Security.Claims;
using System;

namespace SmartGarden.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;


        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            return Ok(new
            {
                UserName = User.Identity!.Name,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Role = User.FindFirstValue(ClaimTypes.Role),
                Nickname = User.FindFirstValue("Nickname")
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return Unauthorized("This user does not exist.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                Console.WriteLine("Invalid password.");
                return Unauthorized("Invalid password.");
            }

            var token = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, (int)user.Role == 0 ? "Admin" : "User"),
            new Claim("Nickname", user.NickName)
                ], CookieAuthenticationDefaults.AuthenticationScheme));

            await HttpContext.SignInAsync(token, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.Now.AddHours(1) });

            return Ok(new { Message = "Zalogowano pomyślnie!", UserId = user.Id });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { Message = "Wylogowano pomyślnie!" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                
                return BadRequest("Użytkownik o podanej nazwie już istnieje.");
            }

            // Opcjonalnie: sprawdzenie czy email też jest zajęty, jeśli masz taką metodę w repozytorium
            // var existingEmail = await _userRepository.GetUserByEmailAsync(request.Email);
            // if (existingEmail != null) return BadRequest("Email jest już w użyciu.");

            
            var passwordHash = _passwordHasher.HashPassword(null!, request.Password);


            var newUser = new User(request.Username, request.Nickname, passwordHash, request.Email, request.Latitude, request.Longitude);

            await _userRepository.AddUser(newUser);

            return Ok(new { Message = "User registered successfully!" });
        }
    }
}
