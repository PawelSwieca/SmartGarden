using System.ComponentModel.DataAnnotations;

namespace SmartGarden.Shared.DTOs
{
    public class PlantProfileLookupDTO
    {
        [Required(ErrorMessage = "No ID provided.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "No name provided.")]
        public string Name { get; set; }
    }
}
