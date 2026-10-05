using System.ComponentModel.DataAnnotations;


namespace SmartGarden.Shared.DTOs
{
    public class CreateGardenDTO
    {
        [Required(ErrorMessage = "Garden name is required.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Garden name cannot consist only of white spaces.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
