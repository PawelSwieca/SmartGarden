using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGarden.Shared.Entities
{
    public enum Species { Vegetable, Fruit, Flower, Herb, Crop }


    [Table("PlantProfiles")]
    public class PlantProfile
    {
        [Key]
        public int Id { get; private set; }

        [Required, MaxLength(100)]
        public string Name { get; init; } = string.Empty;
        public Species Species { get; init ; }

        [MaxLength(500)]
        public string Description { get; init; } = string.Empty;

        public int WateringFrequencyDays { get; init; }
        public int DaysToHarvest { get; init; }

        public PlantProfile() { }

        public PlantProfile(string name, Species species, string description, int wateringDays, int harvestDays)
        {
            Name = name;
            Species = species;
            Description = description;
            WateringFrequencyDays = wateringDays;
            DaysToHarvest = harvestDays;
        }
    }
}
