using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Lingarix_Database;
using Microsoft.EntityFrameworkCore;

namespace NyelvtanuloMVC.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatController : Controller
    {
        private readonly LingarixDbContext DBcontext;

        public StatController(LingarixDbContext context)
        {
            DBcontext = context;
        }
        [HttpGet]
        public IActionResult GetStatistics()
        {
            var stats = DBcontext.users.Select(u => new
            {
                Username = u.Username,
                Points = u.Points
            }).ToList();
            return Ok(stats);
        }

        [HttpPost]
        public IActionResult UpdateScore([FromBody] UserScoreUpdateModel model)
        {
            Lingarix_Database.Entities.Users? user = DBcontext.users.FirstOrDefault(u => u.Username == model.Username);
            if (user == null)
            {
                return NotFound();
            }
            user.Points = model.Points;
            DBcontext.SaveChanges();
            return Ok(user);
        }
        //public IActionResult Statistics()
        //{
        //    var userName = User.Identity.Name; // Bejelentkezett felhasználó neve
        //    var statistics = DBcontext.UserStatistics
        //                            .Where(s => s.UserName == userName)
        //                            .OrderByDescending(s => s.Date)
        //                            .ToList();
        //    return View();
        //}

    }
    public class UserScoreUpdateModel
    {
        public string Username { get; set; }
        public int Points { get; set; }
    }
}
