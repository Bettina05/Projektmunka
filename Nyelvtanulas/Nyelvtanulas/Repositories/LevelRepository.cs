using Lingarix_Database;
using Lingarix_Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nyelvtanulas.Repositories
{
    public class LevelRepository
    {
        private readonly LingarixDbContext DBcontext;

        public LevelRepository(LingarixDbContext context)
        {
            DBcontext = context;
        }
        public async Task<UserLevels> GetLevelByUser(string username)
        {
            return await DBcontext.Levels.FirstOrDefaultAsync(l => l.Username == username);
        }

        public async Task UpdateLevel(UserLevels level)
        {
            DBcontext.Levels.Update(level);
            await DBcontext.SaveChangesAsync();
        }
    }
}
