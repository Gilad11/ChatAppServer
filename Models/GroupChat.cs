
using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.Models
{
    public class GroupChat
    {
        [Key]
        public string GroupId { get; set; } = Guid.NewGuid().ToString(); // NTC
        public string GroupName { get; set; } = "";

        // Relationships
        public List<Message> GroupMessages { get; set; } = new List<Message>();
        public List<User> GroupUsers { get; set; } = new List<User>();
    }

}
