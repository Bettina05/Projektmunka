using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nyelvtanulas.Models;
using System.Security.Cryptography;

namespace Nyelvtanulas.Controllers
{
    public class AccountController : Controller
    {
        private IUserManager userManager;
        private IEncryptionService encryptionService;
        private Models.IAuthenticationService authenticationService;

        public AccountController(IUserManager userManager, IEncryptionService encryptionService, Models.IAuthenticationService authenticationService)
        {
            this.userManager = userManager;
            this.encryptionService = encryptionService;
            this.authenticationService = authenticationService;
        }
        public IActionResult Register()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }

        //Bejelentkezés feldolgozása
        [HttpPost]
        public IActionResult Login(User user, string username, string password)
        {
            var hashedPassword = user.HashPassword(password);
            var users = userManager.GetAll().FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", username);
                // Bejelentkezés után statisztika oldalra irányít
                return RedirectToAction("Index", "Stats");  
            }

            ViewBag.Error = "Hibás felhasználónév vagy jelszó!";
            return View();
        }

        //Captcha validáció
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

        //Kijelentkezés, majd az Index nézetre visszatér
        public IActionResult Logout()
        {
            authenticationService.LogOut();
            return RedirectToAction("Index");
        }

        // Regisztrációs form feldolgozása
        [HttpPost]
        public IActionResult Register(string fullName, string userName, string email, string password, string captchaInput)
        {
            // CAPTCHA ellenőrzése
            var captcha = HttpContext.Session.GetString("CaptchaCode");
            if (captcha != captchaInput)
            {
                ModelState.AddModelError("Captcha", "Hibás kód, próbáld újra!");
                return View();
            }

            // Ide kéne az adatbázisba mentés

            // Regisztráció sikeres
            return RedirectToAction("Success");
        }

        // Sikeres regisztráció után
        public IActionResult Success()
        {
            return View();
        }

        // CAPTCHA kód generálása
        public IActionResult GenerateCaptcha()
        {
            var captcha = GenerateRandomCaptcha();

            // CAPTCHA kód tárolása a session-ben
            HttpContext.Session.SetString("CaptchaCode", captcha);

            // Visszaadjuk a CAPTCHA kódot
            return Json(new { captcha = captcha });
        }

        // Véletlenszerű CAPTCHA kód generálása
        private string GenerateRandomCaptcha()
        {
            var chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            var captcha = new string(Enumerable.Range(0, 6)
                .Select(_ => chars[random.Next(chars.Length)])
                .ToArray());
            return captcha;
        }
    }
}
