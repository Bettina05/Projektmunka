
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
