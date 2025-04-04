using Lingarix_Database;
using Lingarix_Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nyelvtanulas.Repositories
{
    public class BadgeRepository
    {
        private readonly LingarixDbContext DBcontext;

        public BadgeRepository(LingarixDbContext context)
        {
            DBcontext = context;
        }

        public async Task<List<UserBadge>> GetBadgesByUser(string username)
        {
            return await DBcontext.Badges.Where(b => b.Username == username).ToListAsync();
        }

        public async Task AddBadge(UserBadge badge)
        {
            DBcontext.Badges.Add(badge);
            await DBcontext.SaveChangesAsync();
        }
    }
}
