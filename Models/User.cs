using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.Models
{
    public class User
    {
        [Required]
        [MaxLength(100)]
        public string Id { get; set; } = string.Empty;
        public string Password { get; set; } = "0000";
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string ProfilePicture { get; set; } = string.Empty;
        public DateTime LastActiveDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
    }
}
