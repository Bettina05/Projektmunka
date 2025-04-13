
using Microsoft.AspNetCore.Mvc;
using Nyelvtanulas.Models;
using System.Security.Cryptography;
using Lingarix_Database;
using Lingarix_Database.Entities;
using System.Text;


namespace Nyelvtanulas.Controllers
{
    public class AccountController : Controller
    {
        private IUserManager userManager;
        private IEncryptionService encryptionService;
        private IAuthenticationService authenticationService;

        public AccountController(IUserManager userManager, IEncryptionService encryptionService, IAuthenticationService authenticationService)
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

        public IActionResult RegisterUser(Users user)
        {
            user.PasswordHash = encryptionService.HashPassword(user.PasswordHash);
            userManager.Add(user);
            return RedirectToAction("Login"); 
        }
        
        [HttpPost]
        public IActionResult Login(string username, string password, string captcha)
        {
            if (authenticationService.TryLogIn(username, password))
            {
                return RedirectToAction("Index","Home");
            }
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

        public IActionResult Logout()
        {
            authenticationService.LogOut();
            return RedirectToAction("Logout", "Home");
        }
    }
}
