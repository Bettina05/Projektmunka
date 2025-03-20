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
            string username = User.Identity.Name; // Bejelentkezett felhaszn�l� neve
                                                  // A konzolos alkalmaz�s relat�v el�r�si �tja
                                                  // Konzolos alkalmaz�s met�dus�nak megh�v�sa
            Program.StartConsoleApp(username);

            return RedirectToAction("Index", "Home"); // Visszair�ny�t�s az MVC f�oldal�ra
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
