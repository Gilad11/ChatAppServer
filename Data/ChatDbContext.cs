using ChatAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<User>().HasData(
                new User
                {
                    Username = "324973429",
                    Email = "giladnaftali@gmail.com",
                    Password = "Gilad123",
                    ProfilePicture = "https://iva.org.il/Pics/_Players/d91e47be-cf47-6a42-b439-8fa14794a4bd-374f5b84-6a48-e80a-34fd-561df88f7dd2.png",
                    LastActiveDate = DateTime.UtcNow.AddHours(-1),
                    isActive = true
                },
                new User
                {
                Username = "jane_smith",
                Email = "jane.smith@example.com",
                Password = "securepass456",
                ProfilePicture = null,
                LastActiveDate = DateTime.UtcNow.AddDays(-2),
                isActive = false
                },
                new User
                {
                Username = "bob_builder",
                Email = "bob.builder@example.com",
                Password = "construction123",
                ProfilePicture = "https://bootdey.com/img/Content/avatar/avatar3.png",
                LastActiveDate = DateTime.UtcNow.AddMinutes(-15),
                isActive = true
                },
                new User
                {
                Username = "alice_wonder",
                Email = "alice@example.com",
                Password = "alice2023",
                ProfilePicture = "https://bootdey.com/img/Content/avatar/avatar4.png",
                isActive = false
                },
                new User
                {
                Username = "charlie123",
                Email = "charlie@example.net",
                Password = "qwerty",
                ProfilePicture = "https://bootdey.com/img/Content/avatar/avatar5.png",
                LastActiveDate = DateTime.UtcNow.AddHours(-10),
                isActive = true
                },
                new User
                {
                Username = "eve_online",
                Email = "eve.online@example.org",
                Password = "eve123456",
                ProfilePicture = null,
                LastActiveDate = DateTime.UtcNow.AddDays(-1),
                isActive = false
                },
                new User
                {
                Username = "david_b",
                Email = "david.b@example.com",
                Password = "letmein",
                ProfilePicture = "https://bootdey.com/img/Content/avatar/avatar6.png",
                LastActiveDate = DateTime.UtcNow,
                isActive = true
                },
                new User
                {
                Username = "susan_q",
                Email = "susan.q@example.com",
                Password = "ilovecats",
                ProfilePicture = "https://bootdey.com/img/Content/avatar/avatar7.png",
                LastActiveDate = DateTime.UtcNow.AddHours(-3),
                isActive = true
                },
                new User
                {
                Username = "mike_hunt",
                Email = "mike.hunt@example.net",
                Password = "hunter2023",
                ProfilePicture = null,
                LastActiveDate = DateTime.UtcNow.AddDays(-5),
                isActive = false
                },
                new User
                {
                Username = "anna_p",
                Email = "anna.p@example.com",
                Password = "password123",
                ProfilePicture = "https://bootdey.com/img/Content/avatar/avatar9.png",
                LastActiveDate = DateTime.UtcNow.AddMinutes(-30),
                isActive = true
                }
            );*/
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<GroupChat> GroupsChat { get; set; }

        public User? GetUserByUsername(string username)
        {
            return Users.FirstOrDefault(u => u.Username == username);
        }

        public List<GroupChat> GetUserGroupsChat(string username)
        {
            var user = GetUserByUsername(username);
            var groupsChat = user.GroupsList.ToList();
            if (groupsChat == null) groupsChat = new List<GroupChat>();
            return groupsChat;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Users.ToList();
        }

        public void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }

        public void DeleteUser(string username)
        {
            var user = GetUserByUsername(username);
            if (user != null)
            {
                Users.Remove(user);
                SaveChanges();
            }
        }

        public void EditUser(string username, User user)
        {
            var existingUser = Users.FirstOrDefault(u => u.Username == username);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.ProfilePicture = user.ProfilePicture;
                existingUser.LastActiveDate = user.LastActiveDate;
                existingUser.isActive = user.isActive;

                SaveChanges();
            }
        }

        public List<Message>? GetChatMessagesById(string mainUserId, string groupId)
        {
            var chatMessages = GroupsChat.FirstOrDefault(g => g.GroupId == groupId)?.GroupMessages.OrderBy(m => m.SentAt).ToList();
            return chatMessages;
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
            SaveChanges();
        }


        public IEnumerable<GroupChat> GetAllGroupsChat()
        {
            return GroupsChat.ToList();
        }

        public GroupChat? GetGroupChatById(string groupId)
        {
            return GroupsChat.FirstOrDefault(m => m.GroupId == groupId);
        }

        public void AddGroupChat(GroupChat groupChat)
        {
            if (groupChat.GroupName != "")
            {
                GroupsChat.Add(groupChat);
                SaveChanges();
            }
        }

        public void DeleteGroupChat(string groupId)
        {
            var groupChat = GetGroupChatById(groupId);
            if (groupChat != null)
            {
                GroupsChat.Remove(groupChat);
                SaveChanges();
            }
        }

        public void EditGroupChatName(string groupId, string groupName)
        {
            var groupChat = GroupsChat.FirstOrDefault(g => g.GroupId == groupId);
            if (groupChat != null)
            {
                groupChat.GroupName = groupName;
                SaveChanges();
            }
        }
    }
}
