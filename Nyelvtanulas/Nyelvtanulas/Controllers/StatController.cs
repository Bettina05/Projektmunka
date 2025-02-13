using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace NyelvtanuloMVC.Controllers
{
    public class StatsController : Controller
    {
        public IActionResult Index()
        {
            //A konzolos app végén egy stats.txt nevű fájl lesz és azt olvasnánk be
            string filePath = "wwwroot/data/stats.txt";
            string[] stats = System.IO.File.Exists(filePath) ? System.IO.File.ReadAllLines(filePath) : new string[] { "Nincs elérhető statisztika." };
            return View(stats);
        }
    }
}
