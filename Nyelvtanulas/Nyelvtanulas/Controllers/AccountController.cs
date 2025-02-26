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
        private readonly LingarixDbContext DBcontext;
        //A fájl helye
        private readonly string statisztikaFajl = "wwwroot/statistics.txt"; 

        public AccountController(IUserManager userManager, IEncryptionService encryptionService, Models.IAuthenticationService authenticationService, LingarixDbContext DBcontext)
        {
            this.userManager = userManager;
            this.encryptionService = encryptionService;
            this.authenticationService = authenticationService;
            this.DBcontext = DBcontext;
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
        public IActionResult Login(Users user, string username, string password, string captchaInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // CAPTCHA ellenőrzése
            var captcha = HttpContext.Session.GetString("CaptchaCode");
            if (captcha != captchaInput)
            {
                ModelState.AddModelError("Captcha", "Hibás kód, próbáld újra!");
                return View();
            }


            var hashedPassword = HashPassword(password);
            var users = DBcontext.users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user == null)
            {
                ModelState.AddModelError("Captcha", "Hibás kód, próbáld újra!");
                return View();
            }

                HttpContext.Session.SetString("Username", user.Username);
                
                // Bejelentkezés után statisztika oldalra irányít
                return RedirectToAction("Statistics");  
        }

        //Statisztika oldal - csak bejelentkezve látható
        public IActionResult Statistics()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login"); // Ha nincs bejelentkezve, visszadob a login oldalra
            }

            List<string> userStats = new List<string>();

            if (System.IO.File.Exists(statisztikaFajl))
            {
                // Beolvassuk a fájl tartalmát
                var lines = System.IO.File.ReadAllLines(statisztikaFajl);

                // Szűrjük az aktuális bejelentkezett felhasználóra
                userStats = lines
                    .Where(line => line.StartsWith(userName + ";")) // Csak az ő adatait vegye figyelembe
                    .Select(line => line.Replace(";", " - ")) // Szebb formázás
                    .ToList();
            }

            ViewData["Statistics"] = userStats;
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            // CAPTCHA ellenőrzése
            var captcha = HttpContext.Session.GetString("CaptchaCode");
            if (captcha != captchaInput)
            {
                ModelState.AddModelError("Captcha", "Hibás kód, próbáld újra!");
                return View();
            }

            //Ellenőrzi a felhasználónevet
            if (DBcontext.users.Any(u => u.Username == userName))
            {
                ModelState.AddModelError("UserName", "Ez a felhasználónév már foglalt.");
                return View();
            }
            //Jelszó hashelése
            string hashedPassword = HashPassword(password);

            //Új felhasználó hozzáadása
            var user = new Users
            {
                Fullname = fullName,
                Username = userName,
                Email = email,
                PasswordHash = hashedPassword
            };

            DBcontext.users.Add(user);
            DBcontext.SaveChanges();

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
    }
}
