using Microsoft.AspNetCore.Mvc;

namespace Nyelvtanulas.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Tippek nézetet ad vissza
        public IActionResult Tips()
        {
            return View("~/Views/Information/Tips.cshtml");
        }

        // Infomrációk nézetet ad vissza
        public IActionResult Info()
        {
            return View("~/Views/Information/Inform.cshtml");
        }

    }
}


