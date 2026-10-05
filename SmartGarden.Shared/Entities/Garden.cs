using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGarden.Shared.Entities
{
    [Table("Gardens")]
    public class Garden
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Klucz obcy i nawigacja do Użytkownika
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!; // null! wycisza ostrzeżenia kompilatora

        // Relacja 1:N - Ogród ma wiele grządek/roślin
        public ICollection<PlantedCrop> PlantedCrops { get; private set; } = new List<PlantedCrop>();

        public Garden() { }

        public Garden(string name, double latitude, double longitude, int userId)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}