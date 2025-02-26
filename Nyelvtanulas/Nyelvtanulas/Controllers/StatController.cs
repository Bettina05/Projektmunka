using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Lingarix_Database;

namespace NyelvtanuloMVC.Controllers
{
    public class StatsController : Controller
    {
        private readonly LingarixDbContext DBcontext;

        public StatsController(LingarixDbContext context)
        {
            DBcontext = context;
        }

        public IActionResult Index()
        {
            var userName = User.Identity.Name; // Bejelentkezett felhasználó neve
            var statistics = DBcontext.UserStatistics
                                    .Where(s => s.UserName == userName)
                                    .OrderByDescending(s => s.Date)
                                    .ToList();
            return View(statistics);
        }
    }
}
