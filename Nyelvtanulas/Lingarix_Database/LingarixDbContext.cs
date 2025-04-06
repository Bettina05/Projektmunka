using Lingarix_Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingarix_Database
{
    public class LingarixDbContext : DbContext
    {
        public DbSet<Users> users { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }
        public DbSet<UserRangList> UserRangList { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserBadge> Badges { get; set; }
        public DbSet<UserLevels> Levels { get; set; }
        public LingarixDbContext(DbContextOptions<LingarixDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UsersDatabase");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLevels>().HasData(
                new UserLevels { Id = 1, Level = 1, Experience = 0, Description = "Kezdő!" },
                new UserLevels { Id = 2, Level = 2, Experience = 100, Description = "Haladó" },
                new UserLevels { Id = 3, Level = 3, Experience = 300, Description = "Profi" }
            );

            modelBuilder.Entity<UserBadge>().HasData(
                new UserBadge { Id = 1, BadgeName = "Első lépések", Description = "Teljesítetted az első feladatodat", Condition = "Teljesíts egy feladatot" },
                new UserBadge { Id = 2, BadgeName = "Kezdődő széria", Description = "Bejelentkeztél 3 egymást követő napon", Condition = "3 napos bejelentkezési sorozat" }
            );
        }
    }

    
}
