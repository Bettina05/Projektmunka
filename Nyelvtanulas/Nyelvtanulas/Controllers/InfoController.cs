using Microsoft.AspNetCore.Mvc;

namespace Nyelvtanulas.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tips()
        {
            return View();
        }
    }
}


