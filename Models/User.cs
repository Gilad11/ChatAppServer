using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = "";
        public string? Email { get; set; }
        public string Password { get; set; } = "";
        public string? ProfilePicture { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public bool isActive { get; set; } = false;

        // Relationships
        public List<Message> MessagesSent { get; set; } = new List<Message>();
        public List<GroupChat> GroupsList { get; set; } = new List<GroupChat>();
    }

}
