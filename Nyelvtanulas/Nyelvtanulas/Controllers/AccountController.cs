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

        [HttpPost]
        public IActionResult Login(User user, string username, string password)
        {
            var hashedPassword = user.HashPassword(password);
            var users = dbContext.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

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
        public IActionResult Logout()
        {
            authenticationService.LogOut();
            return RedirectToAction("Index");
        }
    }
}
