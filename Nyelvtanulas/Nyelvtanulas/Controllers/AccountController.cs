﻿using Microsoft.AspNetCore.Mvc;
using Nyelvtanulas.Models;

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
                user.PasswordHash = User.HashPassword(user.PasswordHash);
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var hashedPassword = User.HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Stats");  // Bejelentkezés után statisztika oldalra irányít
            }

            ViewBag.Error = "Hibás felhasználónév vagy jelszó!";
            return View();
        }

    }
}
