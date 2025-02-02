using Microsoft.AspNetCore.Mvc;
using Nyelvtanulas.Models;
using System.Security.Cryptography;

namespace Nyelvtanulas.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user, string CaptchaInput)
        {
            string storedCaptcha = HttpContext.Session.GetString("Captcha");
            if (CaptchaInput != storedCaptcha)
            {
                ModelState.AddModelError("", "Helytelen CAPTCHA!");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                user.PasswordHash = user.HashPassword(user.PasswordHash);
               // _context.Users.Add(user);
                //_context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user, string username, string password)
        {
            var hashedPassword = user.HashPassword(password);
            //var users = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Stats");  // Bejelentkezés után statisztika oldalra irányít
            }

            ViewBag.Error = "Hibás felhasználónév vagy jelszó!";
            return View();
        }
        [HttpPost]
        public IActionResult ValidateCaptcha(string captchaInput)
        {
            string correctCaptcha = HttpContext.Session.GetString("CaptchaCode");

            if (captchaInput == correctCaptcha)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Hibás CAPTCHA!" });
            }
        }
    }
}
