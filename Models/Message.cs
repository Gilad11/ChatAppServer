using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ChatAppServer.Models
{
    public class Message
    {
        [Key]
        public string MessageId { get; set; } = Guid.NewGuid().ToString();
        public string SenderId { get; set; } = ""; // Reference to User
        public string GroupId { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public User Sender { get; set; } = null!; //NTC
        public GroupChat? GroupChat { get; set; } //NTC
    }

}
