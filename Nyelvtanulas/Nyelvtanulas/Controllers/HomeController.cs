using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Nyelvtanulas.Models;
using System.Diagnostics;
using LingarixAPP;

namespace Nyelvtanulas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult LaunchConsoleApp()
        {
            string username = User.Identity.Name; // Bejelentkezett felhasználó neve
                                                  // A konzolos alkalmazás relatív elérési útja
                                                  // Konzolos alkalmazás metódusának meghívása
            Program.StartConsoleApp(username);

            return RedirectToAction("Index", "Home"); // Visszairányítás az MVC fõoldalára
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
    }
}
