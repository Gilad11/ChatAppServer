﻿using ChatAppServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChatAppServer.Data
{
    public class ChatAppDbContext : DbContext
    {
        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ChatDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Game> Games { get; set; }

        public string FindGameId(string u1, string u2)
        {
            string gameId = "null";
            int result = String.Compare(u1, u2);
            if (result == 0) return "0";
            if (result == -1) return gameId = u1 + u2;
            if (result == 1) return gameId = u2 + u1;
            return gameId;
        }

        public Game? GetGame(string senderId, string receiverId)
        {
            return Games.FirstOrDefault(g =>
                (g.SenderId == senderId && g.ReceiverId == receiverId) ||
                (g.SenderId == receiverId && g.ReceiverId == senderId));
        }

        public List<Game> GetGames(string userId)
        {
            return Games
                    .Where(g => g.SenderId == userId || g.ReceiverId == userId)
                    .ToList();
        }

        public void SaveGame(Game newGame)
        {
            Game game = Games.FirstOrDefault(g => (g.SenderId == newGame.SenderId && g.ReceiverId == newGame.ReceiverId) || g.ReceiverId == newGame.SenderId && g.SenderId == newGame.ReceiverId);
            if (game != null) {
                Games.Remove(game);
                Games.Add(newGame);
            }
            else
            {
                Games.Add(newGame);
            }
            SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            var users = Users.ToList();
            return users;
        }

        public User? GetUserById(string Id)
        {
            return Users.FirstOrDefault(u => u.Id == Id);
        }

        public List<Message> GetMessagesBetweenUsers(string user1Id, string user2Id)
        {
            return Messages
                .Where(m => (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                            (m.SenderId == user2Id && m.ReceiverId == user1Id))
                .OrderBy(m => m.SentAt) // Sort messages chronologically
                .ToList();
        }

        public async Task AddUser(User user)
        {
            if (user != null)
            {
                Users.Add(user);
                await SaveChangesAsync();
            }
        }

        public async Task EditUser(string Id, User updatedUser)
        {
            var user = Users.FirstOrDefault(u => u.Id == Id);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.ProfilePicture = updatedUser.ProfilePicture;
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;
                await SaveChangesAsync();
            }
        }

        public async Task DeleteUser(string Id)
        {
            var user = Users.FirstOrDefault(u => u.Id == Id);
            if (user != null)
            {
                Users.Remove(user);
                await SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("user already delete!");
            };
        }
        public Message AddMessage(string content, string senderId, string receiverId)
        {
            var message = new Message
            {
                Content = content,
                SenderId = senderId,
                ReceiverId = receiverId,
            };

            Messages.Add(message);
            SaveChanges();
            return message;
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var users = new[]
            {
                new User { Id = "aaa", Name = "a user", ProfilePicture = "https://bootdey.com/img/Content/avatar/avatar1.png", LastActiveDate = DateTime.UtcNow, Email = "aaa@example.com", Password = "1234"},
                new User { Id = "bbb", Name = "b user", ProfilePicture = "https://bootdey.com/img/Content/avatar/avatar2.png", LastActiveDate = DateTime.UtcNow.AddMinutes(-10), Email = "bbb@example.com", Password = "1234"},
                new User { Id = "ccc", Name = "c user", ProfilePicture = "https://bootdey.com/img/Content/avatar/avatar3.png", LastActiveDate = DateTime.UtcNow.AddMinutes(-30), Email = "ccc@example.com", Password = "1234" },
                new User { Id = "ddd", Name = "d user", ProfilePicture = "https://bootdey.com/img/Content/avatar/avatar4.png", LastActiveDate = DateTime.UtcNow.AddHours(-1), Email = "ddd@example.com", Password = "1234" }
            };
            modelBuilder.Entity<User>().HasData(users);

            var messages = new[] {
                new Message { Content = "testme", SenderId = "aaa", ReceiverId = "bbb"},
                new Message { Content = "testyou", SenderId = "bbb", ReceiverId = "aaa"},
                new Message { Content = "testelse", SenderId = "ccc", ReceiverId = "ddd"},
                new Message { Content = "testelse", SenderId = "ddd", ReceiverId = "ccc"},
                new Message { Content = "testme", SenderId = "aaa", ReceiverId = "ccc"},
                new Message { Content = "testelse", SenderId = "ccc", ReceiverId = "aaa"},
                new Message { Content = "test2me", SenderId = "aaa", ReceiverId = "bbb"},
                new Message { Content = "test2you", SenderId = "bbb", ReceiverId = "aaa"},
                };

            modelBuilder.Entity<Message>().HasData(messages);

            //var games = new[]
            //{
            //    new Game
            //    {
            //        Id = 1,
            //        SenderId = "aaa",
            //        ReceiverId = "bbb",
            //        cell1 = 0,
            //        cell2 = 0,
            //        cell3 = 0,
            //        cell4 = 0,
            //        cell5 = 0,
            //        cell6 = 0,
            //        cell7 = 0,
            //        cell8 = 0,
            //        cell9 = 0
            //    }
            //};
            //modelBuilder.Entity<Game>().HasData(games);
        }
    }
}
