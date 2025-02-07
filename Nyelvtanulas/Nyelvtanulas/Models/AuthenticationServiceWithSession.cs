using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Nyelvtanulas.Models
{
    public class AuthenticationServiceWithSession : IAuthenticationService
    {
        private IHttpContextAccessor httpContextAccessor;
        private IUserManager userManager;
        private IEncryptionService encryptionService;

        public AuthenticationServiceWithSession(IHttpContextAccessor httpContextAccessor, IUserManager userManager, IEncryptionService encryptionService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.encryptionService = encryptionService;
        }

        // Van-e a sessionben bejegyzés az emailre

        public bool IsAuthenticated =>
            httpContextAccessor.HttpContext.Session.TryGetValue("email", out byte[] values);

        public string EmailAddress
        {
            get
            {
                httpContextAccessor.HttpContext.Session.TryGetValue("email", out byte[] values);

                if (values is null)
                {
                    return string.Empty;
                }

                return Encoding.UTF8.GetString(values);
            }
        }

        public void LogOut()
        {
            // Töröljük a session tartalmát
            httpContextAccessor.HttpContext.Session.Clear();
        }

        public bool TryLogIn(string email, string password)
        {
            User? foundUser = userManager.GetAll()
                .FirstOrDefault(user => user.Email == email);

            if (foundUser is null) // Ha nincs az email az adatbázisban
            {
                return false;
            }

            string hashedPassword = encryptionService.HashPassword(password);
            if (foundUser.PasswordHash != hashedPassword) // Nem jó jelszót adott meg
            {
                return false;
            }

            // Sikerült, letároljuk a sessionbe
            httpContextAccessor.HttpContext.Session.Set("email", Encoding.UTF8.GetBytes(email));

            return true;
        }
    }
}
