using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Nyelvtanulas.Models;
using System.Diagnostics;


namespace Nyelvtanulas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
