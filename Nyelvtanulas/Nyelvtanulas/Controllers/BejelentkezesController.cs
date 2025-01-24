using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Nyelvtanulas.Controllers
{
    public class BejelentkezesController : Controller
    {
        private IUserManager UserManager;
        private IEncryptionservice encryptionService;
        private IAuthenticationService authenticationService;

        public BejelentkezesController(IUserManager UserManager, IEncryptionService encryptionService, IAuthenticationService authenticationService)
        {
            this.UserManager = UserManager;
            this.encryptionService = encryptionService;
            this.authenticationService = authenticationService;
        }

        // /User
        public IActionResult Index()
        {
            if (!authenticationService.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult RegisterTeachers()
        {
            return View();
        }

        // http://url/Teacher/Login -> API URL
        public IActionResult TeacherLogin()
        {
            return View();
        }

        [Route("Teacher/All")]
        public List<Teacher> ReadAllTeachers()
        {
            return UserManager.GetAll().ToList();
        }

        // Sum/4/5 -> 9

        [Route("Sum/{a}/{b}")]
        public int Sum(int a, int b)
        {
            return a + b;
        }

        // Form beküldésea weboldalról
        public IActionResult RegisterTeacher(Teacher teacher)
        {
            teacher.Password = encryptionService.HashPassword(teacher.Password);
            UserManager.Add(teacher);
            return RedirectToAction("Index");
        }

        public IActionResult Login(string username, string password)
        {
            if (authenticationService.TryLogIn(username, password))
            {
                // Sikerült a bejelentkezés
                return RedirectToAction("Index");
            }

            // Nem sikerült a bejelentkezés
            return RedirectToAction("TeacherLogin");
        }

        public IActionResult Logout()
        {
            authenticationService.LogOut();
            return RedirectToAction("Index");
        }
    }
}
