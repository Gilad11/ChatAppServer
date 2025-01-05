

using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.Models
{
    public class GroupChat
    {
        [Key]
        public Guid GroupId { get; set; }
        public string GroupName { get; set; } = "";
        public Guid GroupCreator { get; set; } // Reference to UserId of creator

        // Relationships
        public List<Message> GroupMessages { get; set; } = new List<Message>();
        public List<User> GroupUsers { get; set; } = new List<User>();
    }

}
