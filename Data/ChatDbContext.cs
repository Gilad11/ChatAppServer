using ChatAppServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ChatAppServer.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<GroupChat> GroupsChat { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
