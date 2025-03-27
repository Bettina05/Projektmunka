using Lingarix_Database;
using System.Text;
using Lingarix_Database.Entities;

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

        public bool IsAuthenticated =>
            httpContextAccessor.HttpContext.Session.TryGetValue("username", out byte[] values);

        public string UserName
        {
            get
            {
                httpContextAccessor.HttpContext.Session.TryGetValue("username", out byte[] values);

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

        public bool TryLogIn(string userName, string password)
        {
            Users? foundUser = userManager.GetAll()
                .FirstOrDefault(user => user.Username == userName);
            
            // Ha nincs az email az adatbázisban
            if (foundUser is null) 
            {
                return false;
            }

            string hashedPassword = encryptionService.HashPassword(password);
            
            // Nem jó jelszót adott meg
            if (foundUser.PasswordHash != hashedPassword)
            {
                return false;
            }

            // Sikerült, letároljuk a sessionbe
            httpContextAccessor.HttpContext.Session.Set("username", Encoding.UTF8.GetBytes(userName));

            return true;
        }
    }
}
