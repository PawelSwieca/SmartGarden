
using System.ComponentModel.DataAnnotations;

namespace SmartGarden.Shared.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username must be unique.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nickname is required.")]
        public string Nickname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
