using Microsoft.AspNetCore.Mvc;

namespace Nyelvtanulas.Controllers
{
    public class CaptchaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ValidateCaptcha([FromBody] CaptchaModel model)
        {
            string correctCaptcha = HttpContext.Session.GetString("CaptchaCode");
            if (model.CaptchaInput == correctCaptcha)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Hibás CAPTCHA!" });
            }
        }
        public class CaptchaModel
        {
            public string CaptchaInput { get; set; }
        }
    }        
}
