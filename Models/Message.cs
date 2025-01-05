using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ChatAppServer.Models
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; } // Reference to User
        public Guid? GroupId { get; set; } // Optional for group messages
        public Guid? RecipientId { get; set; } // Optional for direct messages
        public string Content { get; set; } = "";
        public DateTime SendingTime { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;

        // Relationships
        public User Sender { get; set; } = null!;
        public GroupChat? GroupChat { get; set; }
    }

}
