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
    }
}
