using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SmartGarden.Shared.DTOs
{
    public class PlantCropDTO : IValidatableObject
    {
        [Required(ErrorMessage = "Plant crop ID is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Plant name is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Plant name must be between 1 and 50 characters.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Plant name cannot consist only of white spaces.")]
        public string PlantName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Species is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Species name must be between 1 and 50 characters.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Species name cannot consist only of white spaces.")]
        public string Species { get; set; } = string.Empty;

        [Required(ErrorMessage = "Area is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Area must be a positive number.")]
        public double Area { get; set; }

        [Required(ErrorMessage = "Planting date is required.")]
        public DateTime PlantingDate { get; set; }

        [Required(ErrorMessage = "Estimated harvest date is required.")]
        public DateTime EstimatedHarvestDate { get; set; }

        public bool IsHarvested { get; set; } = false;

        [Range(0, double.MaxValue, ErrorMessage = "Yield weight must be a positive number.")]
        [AllowNull]
        public double HarvestAmount { get; set; }

        [AllowNull]
        public DateTime LastWateredDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EstimatedHarvestDate <= PlantingDate)
            {
                yield return new ValidationResult(
                    "Estimated harvest date must be after the planting date.",
                    new[] { nameof(EstimatedHarvestDate) }
                );
            }
        }
    }
}