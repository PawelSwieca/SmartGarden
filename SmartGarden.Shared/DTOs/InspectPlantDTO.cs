using System.ComponentModel.DataAnnotations;

namespace SmartGarden.Shared.DTOs
{
    public class InspectPlantDTO
    {
        public bool IsHarvested { get; set; }

        [Range(0, 10000, ErrorMessage = "Yield must be a positive number.")]
        public double HarvestAmount { get; set; }
    }
}
