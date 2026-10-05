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
        public string Name { get; private set; } = string.Empty;
        public Species Species { get; private set; }

        [MaxLength(500)]
        public string Description { get; private set; } = string.Empty;

        public int WateringFrequencyDays { get; private set; }
        public int DaysToHarvest { get; private set; }

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
