using Lingarix_Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingarix_Database
{
    public class LingarixDbContext : DbContext
    {
        public DbSet<Users> users { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }
        public DbSet<UserRangList> UserRangList { get; set; }
        public LingarixDbContext(DbContextOptions<LingarixDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UsersDatabase");
        }
    }

    
}
