using Lingarix_Database;
using Lingarix_Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Nyelvtanulas.Controllers
{
    public class BadgeAndLevelController : Controller
    {
        private readonly LingarixDbContext DBcontext;

        public BadgeAndLevelController()
        {
            
        }

        public async Task<List<UserBadge>> GetBadgesByUser(string username)
        {
            return await DBcontext.UserBadge.Where(b => b.Username == username).ToListAsync();
        }

        public async Task AddBadge(UserBadge badge)
        {
            DBcontext.UserBadges.Add(badge);
            await DBcontext.SaveChangesAsync();
        }
    }
}
