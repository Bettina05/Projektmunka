using Microsoft.AspNetCore.Mvc;

namespace Nyelvtanulas.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tips()
        {
            return View("~/Views/Information/Tips.cshtml");
        }

        public IActionResult Info()
        {
            return View("~/Views/Information/Inform.cshtml");
        }

    }
}


