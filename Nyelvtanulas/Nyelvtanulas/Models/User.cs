using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nyelvtanulas.Models
{
    public class User
    {
        /// <summary>
        /// Felhasználó id-je
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Felhasználó teljes neve
        /// </summary>
        [Required]
        public string FullName { get; set; }


        /// <summary>
        /// Felhasználó felhasználóneve
        /// </summary>
        [Required]
        public string Username { get; set; }


        /// <summary>
        /// Felhasználó email címe
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }


        /// <summary>
        /// Jelszó hash-elése
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }
        
        public User users { get; set; }


        /// <summary>
        /// SHA256 titkosítás
        /// </summary>
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
