using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGarden.Shared.Entities
{
   
    [Table("PlantedCrops")]
    public class PlantedCrop
    {
        [Key]
        public int Id { get; private set; }

        public double Area { get; private set; }
        public DateTime PlantingDate { get; private set; }
        public DateTime? LastWateredDate { get; private set; }
        public DateTime? EstimatedHarvestDate { get; private set; }
        public bool IsHarvested { get; private set; } = false;
        public double? YieldWeight { get; private set; }


        [Required]
        public int GardenId { get; private set; }
        [ForeignKey(nameof(GardenId))]
        public Garden Garden { get; private set; } = null!;


        [Required]
        public int PlantProfileId { get; private set; }
        [ForeignKey(nameof(PlantProfileId))]
        public PlantProfile Profile { get; private set; } = null!;

        protected PlantedCrop() { }

        public PlantedCrop(double area, DateTime plantingDate, int gardenId, int plantProfileId, int? daysToHarvest = null)
        {
            Area = area;
            PlantingDate = plantingDate;
            GardenId = gardenId;
            PlantProfileId = plantProfileId;
            LastWateredDate = plantingDate;

            if (daysToHarvest.HasValue)
            {
                EstimatedHarvestDate = plantingDate.AddDays(daysToHarvest.Value);
            }
        }

        public void Harvest(bool isHarvested, double yieldAmount)
        {
            if (yieldAmount < 0)
            {
                throw new ArgumentException("Yield amount cannot be negative!");
            }

            IsHarvested = isHarvested;

            YieldWeight = isHarvested ? yieldAmount : 0;
        }

        public void Water()
        {
            LastWateredDate = DateTime.UtcNow.ToLocalTime();
        }
    }
}