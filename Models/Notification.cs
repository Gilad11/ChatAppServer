namespace ChatAppServer.Models
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        public Guid RecipientId { get; set; } // Reference to User
        public Guid? MessageId { get; set; } // Optional, related message
        public DateTime SendingTime { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;

        // Relationships
        public User Recipient { get; set; } = null!;
    }

}
