using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGarden.Shared.Entities
{
    public enum UserRole { Admin, User }
    public enum UserStatus { Active, Inactive }

    [Table("Users"), Index(nameof(UserName), IsUnique = true), Index(nameof(Latitude), nameof(Longitude), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; private set; }

        [Required, MaxLength(20), DataType(DataType.Text)]
        public string UserName { get; private set; } = string.Empty;

        [Required, MaxLength(256)]
        public string PasswordHash { get; private set; } = string.Empty;

        [Required, MaxLength(50)]
        public string NickName { get; private set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; private set; } = string.Empty;

        [Required]
        public double? Latitude { get; private set; }
        [Required]
        public double? Longitude { get; private set; }

        public UserRole Role { get; private set; } = UserRole.User;
        public UserStatus Status { get; private set; } = UserStatus.Active;
        public DateTime CreatedAt { get; private set; }



        public ICollection<Garden> Gardens { get; private set; } = new List<Garden>();

        protected User() { } 

        public User(string userName, string nickName, string passwordHash, string email, double? latitude, double? longitude, UserRole role = UserRole.User)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            NickName = nickName;
            Email = email;
            Role = role;
            Status = UserStatus.Active;
            CreatedAt = DateTime.UtcNow;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}