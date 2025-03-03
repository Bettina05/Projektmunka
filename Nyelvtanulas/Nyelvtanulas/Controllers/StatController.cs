using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Lingarix_Database;

namespace NyelvtanuloMVC.Controllers
{
    public class StatController : Controller
    {
        private readonly LingarixDbContext DBcontext;

        public StatController(LingarixDbContext context)
        {
            DBcontext = context;
        }

        public IActionResult Statistics()
        {
            var userName = User.Identity.Name; // Bejelentkezett felhasználó neve
            var statistics = DBcontext.UserStatistics
                                    .Where(s => s.UserName == userName)
                                    .OrderByDescending(s => s.Date)
                                    .ToList();
            return View();
        }
    }
}
