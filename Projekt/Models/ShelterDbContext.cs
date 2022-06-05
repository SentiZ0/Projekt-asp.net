using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Data
{
    public class ShelterDbContext : DbContext
    {
        public ShelterDbContext(DbContextOptions options) : base(options) { }
       
        public DbSet<Animals> Animals { get; set; }

        public DbSet<Mail> Mails { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .Property(b => b.UserName)
                .IsRequired(false);
        }
    }
}
