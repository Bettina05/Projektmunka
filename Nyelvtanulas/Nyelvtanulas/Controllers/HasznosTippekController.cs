using Microsoft.AspNetCore.Mvc;

namespace Nyelvtanulas.Controllers
{
    public class HasznosTippekController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
