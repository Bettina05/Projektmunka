using Lingarix_Database;
using Lingarix_Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nyelvtanulas.Repositories;
using IAuthenticationService = Nyelvtanulas.Models.IAuthenticationService;

namespace Nyelvtanulas.Controllers
{
    public class BadgeAndLevelController : Controller
    {
        private readonly BadgeRepository _badgeRepository;
        private readonly LevelRepository _levelRepository;
        private readonly LingarixDbContext DBcontext;
        private IAuthenticationService authenticationService;
        public BadgeAndLevelController(LingarixDbContext context, IAuthenticationService authenticationService)
        {
            DBcontext = context;
            this.authenticationService = authenticationService;
        }
        public BadgeAndLevelController(BadgeRepository badgeRepository, LevelRepository levelRepository)
        {
            _badgeRepository = badgeRepository;
            _levelRepository = levelRepository;
        }
        public IActionResult Badges()
        {
            string username = User.Identity.Name;

            var badges = DBcontext.UserBadges
                .Where(b => b.Username == username)
                .ToList();

            return View(badges);
        }
        public async Task<IActionResult> Badge()
        {
            string username = User.Identity.Name;
            var badges = await _badgeRepository.GetBadgesByUser(username);
            return View(badges);
        }
        public async Task<IActionResult> Level()
        {
            string username = User.Identity.Name;
            var level = await _levelRepository.GetLevelByUser(username);
            return View(level);
        }
    }
}
