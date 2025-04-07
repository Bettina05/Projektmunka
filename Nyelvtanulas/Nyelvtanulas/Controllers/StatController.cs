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

        public IActionResult Ranglist()
        {
            var leaderboard = DBcontext.UserRangList
                .OrderByDescending(l => l.Points)
                .ToList();
            return View(leaderboard);
        }
        public IActionResult Achievement()
        {
            string currentUsername = authenticationService.UserName;
            var achievements = DBcontext.Achievements
                .Where(x => x.Username == currentUsername)
                .OrderByDescending(x => x.DateEarned)
                .ToList();
            return View(achievements);
        }
        public IActionResult Badge()
        {
           return View();
        }
        public IActionResult Level()
        {
            return View();
        }
    }
}
