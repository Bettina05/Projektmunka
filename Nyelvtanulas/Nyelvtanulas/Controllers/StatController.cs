using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Lingarix_Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Nyelvtanulas.Models;
using IAuthenticationService = Nyelvtanulas.Models.IAuthenticationService;
using Lingarix_Database.Entities;

namespace NyelvtanuloMVC.Controllers
{
    public class StatController : Controller
    {
        private readonly LingarixDbContext DBcontext;
        private IAuthenticationService authenticationService;
        public StatController(LingarixDbContext context, IAuthenticationService authenticationService)
        {
            DBcontext = context;
            this.authenticationService = authenticationService;
        }

        public IActionResult Statistics()
        {
            string currentUsername = authenticationService.UserName;
            if (string.IsNullOrEmpty(currentUsername))
            {
                return RedirectToAction("Login", "Account");
            }
            var userStatistics = DBcontext.UserStatistics
                .Where(x => x.Username == currentUsername)
                .OrderByDescending(x => x.Date)
                .ToList();

            return View(userStatistics);
        }
        public IActionResult Ranglist()
        {
            var leaderboard = DBcontext.UserRangList
                .OrderByDescending(l => l.Points)
                .ToList();

            return View(leaderboard);
        }
        public IActionResult Achievement()
        {
            var achievements = DBcontext.Achievements.ToList();
            return View(achievements);
        }
    }
}
