
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nyelvtanulas.Models;
using System.Security.Cryptography;
using Lingarix_Database;
using Lingarix_Database.Entities;
using System.Text;
using System.IO;
using System.Linq;


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
        public List<Users> ReadAllUsers()
        {
            return userManager.GetAll().ToList();
        }
        // Regisztrációs form feldolgozása
        public IActionResult RegisterUser(Users user)
        {
            user.PasswordHash = encryptionService.HashPassword(user.PasswordHash);
            userManager.Add(user);
            ViewBag.Message = "Sikeres regisztráció!";
            return RedirectToAction("Login"); 
        }
        
        //Bejelentkezés feldolgozása
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (authenticationService.TryLogIn(username, password))
            {
                // Sikerült a bejelentkezés
                ViewBag.Message = "Sikeres bejelentkezés!";
                return RedirectToAction("Index","Home");
            }

            // Nem sikerült a bejelentkezés
            ViewBag.Message = "Sikertelen bejelentkezés!";
            return RedirectToAction("Login");
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
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //Kijelentkezés, majd a Logout nézetre visszatér
        public IActionResult Logout()
        {
            authenticationService.LogOut();
            return RedirectToAction("Logout", "Home");
        }
    }
}
