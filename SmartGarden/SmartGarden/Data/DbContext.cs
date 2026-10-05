using Microsoft.EntityFrameworkCore;
using SmartGarden.Shared.Entities;

namespace SmartGarden.Data
{
    // Renamed to avoid collision with Microsoft.EntityFrameworkCore.DbContext
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSet<T> properties here, for example:
        // public DbSet<MyEntity> MyEntities { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Garden> Gardens { get; set; } = null!;
        public DbSet<PlantProfile> PlantProfiles { get; set; } = null!;
        public DbSet<PlantedCrop> PlantedCrops { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlantedCrop>()
             .HasOne(p => p.Profile)
             .WithMany() 
             .HasForeignKey(p => p.PlantProfileId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlantProfile>().HasData(
                    new { Id = 1, Name = "Pomidor Malinowy", Species = Species.Vegetable, Description = "Wysoka odmiana, wymaga palikowania.", WateringFrequencyDays = 2, DaysToHarvest = 80 },
                    new { Id = 2, Name = "Truskawka", Species = Species.Fruit, Description = "Odmiana wczesna, bardzo słodka.", WateringFrequencyDays = 1, DaysToHarvest = 45 },
                    new { Id = 3, Name = "Bazylia", Species = Species.Herb, Description = "Aromatyczne zioło, idealne do sałatek.", WateringFrequencyDays = 3, DaysToHarvest = 30 }
                );
            modelBuilder.Entity<PlantedCrop>().HasData(
                      new
                      {
                          Id = 1,
                          GardenId = 1,
                          PlantProfileId = 1,
                          Area = 5.0,
                          PlantingDate = new DateTime(2026, 5, 1),
                          LastWateredDate = new DateTime(2026, 5, 1),
                          EstimatedHarvestDate = new DateTime(2026, 5, 1).AddDays(80),
                          IsHarvested = false

                      },
                      new
                      {
                            Id = 2,
                            GardenId = 1,
                            PlantProfileId = 3, 
                            Area = 2.5,
                            PlantingDate = new DateTime(2026, 5, 15),
                            LastWateredDate = new DateTime(2026, 5, 15),
                            EstimatedHarvestDate = new DateTime(2026, 5, 15).AddDays(30),
                            IsHarvested = false
                      }
                );
        }
    }
}
