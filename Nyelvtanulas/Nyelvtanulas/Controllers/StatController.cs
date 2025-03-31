using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Lingarix_Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Nyelvtanulas.Models;
using IAuthenticationService = Nyelvtanulas.Models.IAuthenticationService;

namespace NyelvtanuloMVC.Controllers
{
    [ApiController]
    public class StatController : Controller
    {
        private readonly LingarixDbContext DBcontext;
        private IAuthenticationService authenticationService;
        public StatController(LingarixDbContext context, IAuthenticationService authenticationService)
        {
            //// új beszúrás
            //DBcontext.UserStatistics.Add(new Lingarix_Database.Entities.UserStatistics() { });
            //DBcontext.SaveChanges();
            DBcontext = context;
            this.authenticationService = authenticationService;
        }

        //[HttpPost]
        //public IActionResult UpdateScore([FromBody] UserScoreUpdateModel model)
        //{
        //    Lingarix_Database.Entities.Users? user = DBcontext.users.FirstOrDefault(u => u.Username == model.Username);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    //user.Points = model.Points;
        //    DBcontext.SaveChanges();
        //    return Ok(user);
        //}
        [Route("Stat/Statistics")]
        public IActionResult Statistics()
        {
            var userName = authenticationService.UserName; // Bejelentkezett felhasználó neve
            var statistics = DBcontext.UserStatistics
                                    .Where(s => s.UserName == userName)
                                    .OrderByDescending(s => s.Date)
                                    .ToList();
            return View(statistics);
        }

    }
}
