using Microsoft.AspNetCore.Mvc;
using Nyelvtanulas.Models;
using System.Diagnostics;

namespace Nyelvtanulas.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("api/chrome")]
        public void StartChrome()
        {
            Process.Start("notepad");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
