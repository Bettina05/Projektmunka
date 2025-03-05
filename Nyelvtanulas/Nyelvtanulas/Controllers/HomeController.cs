using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Nyelvtanulas.Models;
using System.Diagnostics;

namespace Nyelvtanulas.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("api/app")]
        public void StartChrome()
        {
            Process.Start("C:\\Users\\User\\Documents\\Gunics Bettina 13.B\\Technikusi 2025\\KonzolLingarix\\Lingarix\\Lingarix\\Lingarix.sln");
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
