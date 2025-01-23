using Microsoft.AspNetCore.Mvc;

namespace Nyelvtanulas.Controllers
{
    public class InformacioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
