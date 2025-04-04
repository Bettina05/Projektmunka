using Microsoft.EntityFrameworkCore;
using Lingarix_Database.Entities;

namespace LingarixApplication
{
    public class LingarixDbContext : DbContext
    {
        public LingarixDbContext(DbContextOptions<LingarixDbContext> options) : base(options) { }

        public DbSet<UserStatistics> UserStatistics { get; set; }
        public DbSet<UserRangList> userRangLists { get; set; }
    }
}
