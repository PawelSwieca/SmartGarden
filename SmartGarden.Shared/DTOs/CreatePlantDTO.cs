using SmartGarden.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartGarden.Shared.DTOs
{
    public class CreatePlantDTO
    {
        [Required(ErrorMessage = "Brak identyfikatora ogrodu.")]
        public int GardenId { get; set; }

        [Required(ErrorMessage = "Musisz wybrać roślinę z listy.")]
        public int PlantProfileId { get; set; } 

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Area must be greater than zero.")]
        public double Area { get; set; }

        [Required]
        public DateTime PlantingDate { get; set; } = DateTime.UtcNow; 

        [Required]
        public DateTime EstimatedHarvestDate { get; set; } = DateTime.UtcNow.AddYears(1);
    }
}
