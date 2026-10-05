using System.ComponentModel.DataAnnotations;

namespace SmartGarden.Shared.DTOs
{
    public class UpdateGardenNameDTO
    {
        [Required(ErrorMessage = "Garden name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be under 100 characters.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Name cannot be just empty spaces.")]
        public string Name { get; set; } = string.Empty;
    }
}
