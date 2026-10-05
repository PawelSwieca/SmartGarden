using System.ComponentModel.DataAnnotations;

namespace SmartGarden.Shared.DTOs
{
    public class CreatePlantProfileDTO
    {
        [Required]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Plant name cannot consist only of white spaces.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Species { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Description cannot consist only of white spaces.")]
        public string? Description { get; set; }

        [Range(1, 100, ErrorMessage = "Watering frequency must be a positive integer between 1 and 100.")]
        public int? WateringFrequencyDays { get; set; }

        public int? DaysToHarvest { get; set; }

    }
}
