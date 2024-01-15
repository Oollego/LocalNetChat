using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpChat.Data.Entities
{
    internal class DataContext : DbContext
    {
        public DbSet<UserSettings> Settings { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<UserMessage> UserMessages { get; set; } = null!; 
        public DataContext(DbContextOptions options) : base(options)
        {
          
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSettings>().HasData(new UserSettings
            {
                Id = Guid.NewGuid(),
                Name = "UserName",
                Surname = "",
                FolderPath = Directory.GetCurrentDirectory() + "\\Downloads",
                IsAvatarAdded = false,
                Port = 10000
            });

            modelBuilder.Entity<Contact>().HasData(new Contact
            {
               
                Id = Guid.NewGuid(),
                Name = "UserName",
                Surname = "",
                IpAddress = "127.0.0.1",
                IsAvatarAdded = false,
                                
            });
        }
    }
}
