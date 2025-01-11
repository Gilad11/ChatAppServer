using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; } = "";
        public string? Email { get; set; }
        public string Password { get; set; } = "0000";
        public string? ProfilePicture { get; set; }
        public DateTime LastActiveDate { get; set; } = DateTime.UtcNow;
        public bool isActive { get; set; } = false;

        // Relationships
        public List<GroupChat> GroupsList { get; set; } = new List<GroupChat>();
    }

}
