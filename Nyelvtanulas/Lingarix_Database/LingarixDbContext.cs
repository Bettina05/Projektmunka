using Lingarix_Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingarix_Database
{
    public class LingarixDbContext : DbContext
    {
        public LingarixDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Users> users { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }
        //public LingarixDbContext(DbContextOptions<LingarixDbContext> options)
        //: base(options)
        //{
            
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Users>()
        //        .HasKey( user => user.Id);

        //    modelBuilder.Entity<Users>().
        //        HasIndex(user => user.Username)
        //        .IsUnique();

        //    modelBuilder.Entity<Users>()
        //        .HasIndex(user => user.Email)
        //        .IsUnique();

        //    modelBuilder.Entity<Users>()
        //        .Property(u => u.Username)
        //        .IsRequired()
        //        .HasMaxLength(50);

        //    modelBuilder.Entity<Users>()
        //        .Property(u => u.PasswordHash)
        //        .IsRequired();

        //    modelBuilder.Entity<Users>()
        //        .Property(u => u.Email)
        //        .IsRequired()
        //        .HasMaxLength(100);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UsersDatabase");
        }
    }

    
}
