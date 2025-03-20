using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Lingarix_Database;
using Microsoft.EntityFrameworkCore;

namespace Nyelvtanulas.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly LingarixDbContext DBcontext;

        public UsersController(LingarixDbContext context)
        {
            DBcontext = context;
        }

        [HttpGet("{username}")]
        public IActionResult GetUser(string username)
        {
            var user = DBcontext.users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound("Felhasználó nem található.");
            }

            return Ok(new { user.Username });
        }
    }
}
